using APARTMENT_API.DTOs.Response;
using APARTMENT_API.Exceptions;
using APARTMENT_API.Models;
using APARTMENT_API.Repositories.Interfaces;
using APARTMENT_API.Services.Interfaces;
using AutoMapper;

namespace APARTMENT_API.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IRolePermissionRepository _repository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;
        public RolePermissionService(IRolePermissionRepository repository, IRoleRepository roleRepository, IPermissionRepository permissionRepository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _permissionRepository = permissionRepository;
            _roleRepository = roleRepository;
        }
        public async Task<RolePermissionsResDto?> GetRolesPermissionsAsync(int roleId)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);
            if (role == null)
                throw new NotFoundException("Role Not Found");
            var permissions = await _repository.GetRolePermissionsAsync(roleId);
            return new RolePermissionsResDto
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Permissions = _mapper.Map<List<PermissionResDto>>(permissions)
            };
        }

        public async Task<bool> AssignPermissionAsync(int roleId, int permissionId)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);
            if (role == null)
                throw new NotFoundException("Role Not Found.");

            var permission = await _permissionRepository.GetByIdAsync(permissionId);
            if (permission == null)
                throw new NotFoundException("Permission Not Found");

            if (role.IsActive != 1)
                throw new InvalidOperationException("Role is inactive");

            var exists = await _repository.HasPermissionAsync(roleId, permissionId);
            if (exists)
                throw new BadRequestException("Role already has this permission");

            var rolePermission = new ApplicationRolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId,
            };

            await _repository.AddRolePermissionAsync(rolePermission);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AssignPermissionsAsync(int roleId, List<int> permissionIds)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);
            if (role == null)
                throw new NotFoundException("Role Not Found");

            if (role.IsActive != 1)
                throw new InvalidOperationException($"Role {role.Name} is inactive");

            if (permissionIds == null || permissionIds.Count == 0)
                throw new BadRequestException("At least one permission is Required");

            permissionIds = permissionIds.Distinct().ToList();
            var newPermissions = new List<ApplicationRolePermission>();

            foreach (var permissionId in permissionIds)
            {
                var permission = await _permissionRepository.GetByIdAsync(permissionId);
                if (permission == null)
                    throw new NotFoundException($"Permission ID {permissionId} Not Found");

                var exists = await _repository.HasPermissionAsync(roleId, permissionId);
                if (!exists)
                {
                    newPermissions.Add(new ApplicationRolePermission { RoleId = roleId, PermissionId = permissionId });
                }
            }

            if (newPermissions.Count == 0)
                throw new BadRequestException("No new permissions to assign (Permissions might already exist).");

            await _repository.AddRolePermissionsAsync(newPermissions);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemovePermissionAsync(int roleId, int permissionId)
        {
            var rolePermission = await _repository.GetRolePermissionAsync(roleId, permissionId);
            if (rolePermission == null)
                throw new BadRequestException("Role permission mapping not found");

            _repository.RemoveRolePermission(rolePermission);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemovePermissionsAsync(int roleId, List<int> permissionIds)
        {
            var role = await _roleRepository.GetByIdAsync(roleId);
            if (role == null)
                throw new NotFoundException("Role Not Found");

            if (permissionIds == null || permissionIds.Count == 0)
                throw new BadRequestException("At least one permission is required");

            permissionIds = permissionIds.Distinct().ToList();

            // Check Permission Really has in table (Optional)
            foreach (var permissionId in permissionIds)
            {
                var permission = await _permissionRepository.GetByIdAsync(permissionId);
                if (permission == null)
                    throw new NotFoundException($"Permission ID {permissionId} not found");
            }

            var existingPermissions = await _repository.GetRolePermissionEntitiesAsync(roleId);

            var permissionsToRemove = existingPermissions
                .Where(rp => permissionIds.Contains(rp.PermissionId))
                .ToList();

            if (permissionsToRemove.Count == 0)
                throw new BadRequestException("Role does not have any of the specified permissions");

            _repository.RemoveRolePermissions(permissionsToRemove);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}

