using APARTMENT_API.DTOs.Response;
using APARTMENT_API.Repositories.Interfaces;
using APARTMENT_API.Exceptions;
using APARTMENT_API.Services.Interfaces;
using AutoMapper;
using APARTMENT_API.Models;

namespace APARTMENT_API.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public UserRoleService(IUserRoleRepository repository,IUserRepository userRepository,IRoleRepository roleRepository,IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        public async Task<UserRoleResDto?> GetUserRolesAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("User Not Found");
            var roles = await _repository.GetUserRolesAsync(userId);
            return new UserRoleResDto
            {
                UserId = user.Id,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                Roles = _mapper.Map<List<RoleResDto>>(roles)
            };
        }

        public async Task<bool> AssignRoleAsync(int userId,int roleId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("User Not Found.");
            var role = await _roleRepository.GetByIdAsync(roleId);
            if (role == null)
                throw new NotFoundException("Role Not Found");
            if (role.IsActive != 1)
                throw new InvalidOperationException("Role is inactive");
            var exists = await _repository.HashRoleAsync(userId, roleId);
            if (exists)
                throw new BadRequestException("User already has this role");
            var userRole = new ApplicationUserRole
            {
                UserId = userId,
                RoleId = roleId,
            };

            await _repository.AddUserRoleAsync(userRole);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignRolesAsync(int userId,List<int> roleIds)
        {
            var user = await _userRepository.GetUserByIdAsync (userId);
            if (user == null)
                throw new NotFoundException("User Not Found");
            if (roleIds == null || roleIds.Count == 0)
                throw new NotFoundException("At least one role is Required");
            roleIds = roleIds.Distinct().ToList();
            var newRoles = new List<ApplicationUserRole>();
            foreach (var roleId in roleIds)
            {
                var role = await _roleRepository.GetByIdAsync (roleId);
                if (role == null)
                    throw new NotFoundException($"Role ID {roleId} Not Found");
                if (role.IsActive != 1)
                    throw new InvalidOperationException($"Role {role.Name} is inactive");
                var exists = await _repository.HashRoleAsync (userId, roleId);
                if (!exists)
                {
                    newRoles.Add(new ApplicationUserRole { UserId = userId, RoleId = roleId });
                }
            }
            if (newRoles.Count == 0)
                throw new BadRequestException("No new roles to assign (Roles might already exist).");
            await _repository.AddUserRolesAsync(newRoles);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRoleAsync(int userId, int roleId)
        {
            var userRole = await _repository.GetUserRoleAsync(userId, roleId);
            if (userRole == null)
                throw new BadRequestException("User role not found");
            _repository.RemoveUserRole(userRole);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveRolesAsync(int userId,List<int> roleIds)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new NotFoundException("User Not Found");
            if (roleIds == null || roleIds.Count == 0)
                throw new BadRequestException("At least one role is required");
            roleIds = roleIds.Distinct().ToList();
            foreach (var roleId in roleIds)
            {
                var role = await _roleRepository.GetByIdAsync(roleId);
                if (role == null)
                    throw new NotFoundException($"Role ID {roleId} not found");
            }
            var existingRoles = await _repository.GetUserRoleEntitiesASync(userId);
            var rolesToRemove = existingRoles
                .Where(userRole => roleIds.Contains(userRole.RoleId))
                .ToList();
            if (rolesToRemove.Count == 0)
                throw new BadRequestException("User does not have any of the specified roles");
            _repository.RemoveUserRoles(rolesToRemove);
            await _repository.SaveChangesAsync();
            return true;
        }

    }
}
