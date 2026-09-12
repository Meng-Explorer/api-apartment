using APARTMENT_API.Configurations;
using APARTMENT_API.Repositories;
using APARTMENT_API.Repositories.Interfaces;
using APARTMENT_API.Services;
using APARTMENT_API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Database Connection
var conStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseOracle(conStr);
});

builder.Services.AddControllers();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperConfiguration).Assembly);

// Add Scoped Repositories
builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();
builder.Services.AddScoped<IFloorRepository, FloorRepository>();
builder.Services.AddScoped<IAuthorizationRepository, AuthorizationRepository>();

// Add Scoped Services
builder.Services.AddScoped<IBuildingService, BuildingService>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<IFloorService, FloorService>();
builder.Services.AddOpenApiDocument(options =>
{
    options.DocumentName = "v1";
    options.Title = "APARTMENT API";
    options.Version = "v1";
    options.AddSecurity("JWT", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
    {
        Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = NSwag.OpenApiSecurityApiKeyLocation.Header,
        Description = "please input Bearer into[Authorization]"
    });

    // Config Security go to Controller/Action that have Attribute [Authorize]
    options.OperationProcessors.Add(
        new NSwag.Generation.Processors.Security.AspNetCoreOperationSecurityScopeProcessor("JWT")
    );
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
    );
});

// Configure Authentication
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!)
        )
    };
});

var app = builder.Build();
app.UseMiddleware<APARTMENT_API.Middlewares.ExceptionMiddleware>();

// 2. Security Protocol
app.UseHttpsRedirection();

// 3. Routing
app.UseRouting();

// 4. CORS
app.UseCors("AllowAll");

app.Use(async (context, next) =>
{
    if (!context.Request.Headers.ContainsKey("Authorization"))
    {
        if (context.Request.Query.TryGetValue("access_token", out var tokenValues))
        {
            var token = tokenValues.FirstOrDefault();
            if (!string.IsNullOrEmpty(token))
            {
                context.Request.Headers["Authorization"] = $"Bearer {token}";
            }
        }
    }
    await next();
});

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized && !context.Response.HasStarted)
    {
        context.Response.ContentType = "application/json";
        var payload = new
        {
            Success = false,
            StatusCode = 401,
            Data = new { }
        };
        var json = System.Text.Json.JsonSerializer.Serialize(payload);
        await context.Response.WriteAsync(json);
    }
});

// 7. Identity verification
app.UseAuthentication();
app.UseAuthorization();

// 8. API Documentation UI (NSwag)
app.UseOpenApi();
app.UseSwaggerUi(options =>
{
    options.Path = "/swagger";
    options.DocumentPath = "/swagger/v1/swagger.json";
});

// 9. Map Endpoints
app.MapControllers();

app.Run();