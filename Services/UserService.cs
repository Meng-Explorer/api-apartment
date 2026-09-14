using APARTMENT_API.DTOs.Request;
using APARTMENT_API.DTOs.Response;
using APARTMENT_API.Exceptions;
using APARTMENT_API.Helpers;
using APARTMENT_API.Models;
using APARTMENT_API.Repositories.Interfaces;
using APARTMENT_API.Services.Interfaces;
using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APARTMENT_API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<ApplicationUser> _passwordHasher;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUserRepository repository,
            IMapper mapper,
            IUserRoleRepository userRoleRepository,
            IConfiguration configuration,
            PasswordHasher<ApplicationUser> passwordHasher,
            ILogger<UserService> logger
            )
        {
            _repository = repository;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
            _logger = logger;

        }
        public async Task<PageResult<UserResDto>> GetUsersByPageAsync(int page = 1, int pageSize = 10)
        {
            var user = await _repository.GetUserByPageAsync(page, pageSize);
            if(user == null)
            {
                throw new NotFoundException("Data Not found");
            }
            return new PageResult<UserResDto>
            {
                PageNumber = user.PageNumber,
                PageSize = user.PageSize,
                TotalRecords = user.TotalRecords,
                TotalPages = user.TotalPages,
                Data = _mapper.Map<List<UserResDto>>(user.Data)
            };

                
        }

        public async Task<List<UserResDto>> GetUsersAsync()
        {
            var data = await _repository.GetUsersAsync();
            return _mapper.Map<List<UserResDto>>(data);
        }

        public async Task<UserResDto?> GetUserByIdAsync(int userId)
        {
            var data = await _repository.GetUserByIdAsync(userId);
            return _mapper?.Map<UserResDto>(data);
        } 

        private string HashPassword(ApplicationUser user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        private PasswordVerificationResult VerificationResult(ApplicationUser user, string password)
        {
            return _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                password
              );
        }

        private string CreateToken(ApplicationUser user , List<string> userRoles)
        {
            var claims = new List<Claim> { 
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (ClaimTypes.Name, user.Username),
                new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (ClaimTypes.Email,user.Email ?? ""),
                new ("FullName",user.FullName ?? "")

            };
            foreach(var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole ?? ""));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<LoginResDto> Login(LoginReqDto request)
        {
            var username = request.UserName.Trim().ToLowerInvariant();
            var user = await _repository.GetUserByNameAsync(request.UserName);
            if(user == null)
            {
                throw new BadRequestException("Invalid username or password");
            }
            if(user.IsActive == 0)
            {
                throw new BadRequestException("Account is inactive");
            }
            var passwordValid = VerificationResult(user, request.Password);
            if(passwordValid == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid password or username");
            }
            var userRoles = await _userRoleRepository.GetUserRolesAsync(user.Id);
            var roles = userRoles.Select(x => x.Name).ToList();
            var token = CreateToken(user, roles);
            var result = new LoginResDto()
            {
                User = _mapper.Map<UserResDto>(user),
                Token = token,
                Roles = userRoles

            };
            return result;

        }

        public async Task<UserResDto> Register(RegisterReqDto request)
        {
            var username = request.Username.Trim().ToLowerInvariant();
            var user = await _repository.GetUserByNameAsync(request.Username);
            if (user != null)
            {
                throw new BadRequestException("Username already Exist");
            }
            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                var emailExists = await _repository.GetUserByEmailAsync(request.Email);
                if (emailExists != null)
                {
                    throw new BadRequestException("Email already Exist");
                }
            }
            var newUser = _mapper.Map<ApplicationUser>(request);
            newUser.PasswordHash = HashPassword(newUser, request.Password);
            await _repository.Register(newUser);
            return _mapper.Map<UserResDto>(newUser);
        }
        public async Task<LoginResDto> ExternalLoginAsync(ExternalAuthDto request)
        {
            string email = string.Empty;
            string fullName = string.Empty;

            //Compare Token follow Provider
            if (request.Provider.ToLower() == "google")
            {
                var payload = await VerifyGoogleToken(request.IdToken);
                if (payload == null || string.IsNullOrEmpty(payload.Email))
                    throw new BadRequestException("Invalid Google Token or Email not provided");

                email = payload.Email;
                fullName = payload.Name;
            }
            else if (request.Provider.ToLower() == "facebook")
            {
                var payload = await VerifyFacebookToken(request.IdToken);
                if (payload == null || string.IsNullOrEmpty(payload.Email))
                    throw new BadRequestException("Invalid Facebook Token or Email not provided");

                email = payload.Email;
                fullName = payload.Name;
            }else if(request.Provider.ToLower() == "apple")
            {
                var payload = await VerifyAppleToken(request.IdToken);
                if (payload == null || string.IsNullOrEmpty(payload.Email))
                    throw new BadRequestException("Invalid Apple Token");
                email =payload.Email;
                fullName = !string.IsNullOrEmpty(request.FullName) ? request.FullName : "Apple User";
            }
            else
            {
                throw new BadRequestException("Invalid Provider");
            }

            // Normalize so lookup and storage always agree (repository lookups compare on Trim().ToLowerInvariant())
            email = email.Trim().ToLowerInvariant();

            // ២. check Email from Database that have or not
            var user = await _repository.GetUserByEmailAsync(email);

            if (user == null)
            {
                // don't have user login auto (Auto-Register)
                user = new ApplicationUser
                {
                    Email = email,
                    // Create Username don't duplicate username cut from email
                    Username = email.Split('@')[0] + "_" + Guid.NewGuid().ToString().Substring(0, 5),
                    FullName = fullName,
                    IsActive = 1,
                    CreatedAt = DateTime.UtcNow
                };

                // cause Database must be has PasswordHash we create Password fake password (Dummy Password)
                var dummyPassword = Guid.NewGuid().ToString();
                user.PasswordHash = HashPassword(user, dummyPassword);

                await _repository.Register(user);

                // Optional: we can Assign Default Role (Example: "User") 
            }
            else if (user.IsActive == 0)
            {
                throw new BadRequestException("Account is inactive");
            }

            //  call Roles និងបង្កើត JWT Token ថ្មី (Custom Token របស់អ្នក)
            var userRoles = await _userRoleRepository.GetUserRolesAsync(user.Id);
            var roles = userRoles.Select(x => x.Name).ToList(); 

            var token = CreateToken(user, roles);

            return new LoginResDto
            {
                User = _mapper.Map<UserResDto>(user),
                Token = token,
                Roles = userRoles 
            };
        }

        private async Task<GoogleJsonWebSignature.Payload?> VerifyGoogleToken(string idToken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _configuration["Authentication:Google:ClientId"]! }
                };
                return await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Google token validation failed");
                return null;
            }
        }
        private async Task<FacebookTokenValidationResult?> VerifyFacebookToken(string accessToken)
        {
            try
            {
                using var httpClient = new HttpClient();
                var appId = _configuration["Authentication:Facebook:AppId"];
                var appSecret = _configuration["Authentication:Facebook:AppSecret"];

                // Verify the token was issued for this app before trusting it (Graph API debug_token)
                var appAccessToken = $"{appId}|{appSecret}";
                var debugTokenEndPoint = $"https://graph.facebook.com/debug_token?input_token={accessToken}&access_token={appAccessToken}";
                var debugResponse = await httpClient.GetAsync(debugTokenEndPoint);
                if (!debugResponse.IsSuccessStatusCode)
                {
                    _logger.LogWarning("Facebook debug_token call failed with status {StatusCode}", debugResponse.StatusCode);
                    return null;
                }

                var debugContent = await debugResponse.Content.ReadAsStringAsync();
                var debugResult = JsonConvert.DeserializeObject<FacebookDebugTokenResponse>(debugContent);
                if (debugResult?.Data == null || !debugResult.Data.IsValid || debugResult.Data.AppId != appId)
                {
                    _logger.LogWarning("Facebook token is invalid or was issued for a different app");
                    return null;
                }

                // Call Graph API of Facebook fetch data
                var verifyTokenEndPoint = $"https://graph.facebook.com/me?fields=id,email,name&access_token={accessToken}";
                var response = await httpClient.GetAsync(verifyTokenEndPoint);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<FacebookTokenValidationResult>(content);
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Facebook token validation failed");
                return null;
            }
        }
        private async Task<AppleTokenValidationResult?> VerifyAppleToken(string idToken)
        {
            try
            {
                using var httpClient = new HttpClient();
                // Fetch Public Keys From Apple for compare Token
                var response = await httpClient.GetAsync("https://appleid.apple.com/auth/keys");
                var json = await response.Content.ReadAsStringAsync();

                var keys = new JsonWebKeySet(json).GetSigningKeys();

                var validationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "https://appleid.apple.com",
                    ValidAudience = _configuration["Authentication:Apple:ClientId"],
                    IssuerSigningKeys = keys,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true
                };

                var handler = new JwtSecurityTokenHandler();
                // Compare (Validate) it from Apple and don't expire
                var principal = handler.ValidateToken(idToken, validationParameters, out _);

                return new AppleTokenValidationResult
                {
                    Email = principal.FindFirstValue(ClaimTypes.Email) ?? principal.FindFirstValue("email") ?? "",
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Apple token validation failed");
                return null;
            }
        }

    }
    // Must be out of Class Parent cause easy for control
    // Class Help for Deserialize Data from Facebook Graph API
    public class FacebookTokenValidationResult
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    // For Facebook debug_token response (used to verify the token was issued for our app)
    public class FacebookDebugTokenResponse
    {
        [JsonProperty("data")]
        public FacebookDebugTokenData? Data { get; set; }
    }

    public class FacebookDebugTokenData
    {
        [JsonProperty("app_id")]
        public string AppId { get; set; } = string.Empty;
        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }
    }

    // For Apple
    public class AppleTokenValidationResult
    {
        public string Email { get; set; } = string.Empty;
    }


}
