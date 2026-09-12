using APARTMENT_API.DTOs.Request;
using APARTMENT_API.DTOs.Response;
using APARTMENT_API.Exceptions;
using APARTMENT_API.Models;
using APARTMENT_API.Repositories.Interfaces;
using APARTMENT_API.Services.Interfaces;
using AutoMapper;

namespace APARTMENT_API.Services
{
    public class PermissionService​ : IPermissionService
    {
        private readonly IPermissionRepository _repository;
        private readonly IMapper _mapper;
        public PermissionService(IPermissionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<PermissionResDto>> GetAllAsync()
        {
            var permissions = await _repository.GetAllAsync();
            return _mapper.Map<List<PermissionResDto>>(permissions);
        }

        public async Task<PermissionResDto?> GetByIdAsync(int Id)
        {
            var permission = await _repository.GetByIdAsync(Id);
            if (permission == null)
                throw new NotFoundException("Permission Not Found");
            return _mapper.Map<PermissionResDto>(permission);
        }

        public async Task<PermissionResDto> CreateAsync(PermissionReqDto request)
        {
            var name = request.Name.Trim();
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Permission name is Required");
            }
            var exists = await _repository.ExistsByNameAsync(name);
            if (exists)
            {
                throw new InvalidOperationException("Permission name already exists");
            }
            var permission = _mapper.Map<ApplicationPermission>(request);
            await _repository.AddAsync(permission);
            await _repository.SaveChangesAsync();
            return _mapper.Map<PermissionResDto>(permission);
        }

        public async Task<PermissionResDto?> UpdateAsync(int Id, PermissionReqDto request)
        {
            var permission = await _repository.GetByIdAsync(Id);
            if (permission == null)
                throw new NotFoundException("Permission Not Found");

            var name = request.Name.Trim();
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Permission name is Required");
            }

            var exists = await _repository.ExistsByNameAsync(name, Id);
            if (exists)
            {
                throw new InvalidOperationException("Permission name already exists");
            }
            _mapper.Map(request, permission);
            // If void must be use await
            _repository.Update(permission);
            await _repository.SaveChangesAsync();
            return _mapper.Map<PermissionResDto>(permission);

        }
        public async Task<bool> DeleteAsync(int Id)
        {
            var permission = await _repository.GetByIdAsync(Id);
            if (permission == null)
                throw new NotFoundException("Permission not Found");
            var hasRoles = await _repository.HasRolesAsync(Id);
            if (hasRoles)
            {
                throw new InvalidOperationException("Cannot delete this permission because it is assigned to one or more users.");
            }
            _repository.Delete(permission);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
