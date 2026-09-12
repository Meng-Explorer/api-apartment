using APARTMENT_API.DTOs.Request;
using APARTMENT_API.DTOs.Response;
using APARTMENT_API.Exceptions;
using APARTMENT_API.Models;
using APARTMENT_API.Repositories.Interfaces;
using APARTMENT_API.Services.Interfaces;
using AutoMapper;

namespace APARTMENT_API.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<RoleResDto>> GetAllAsync()
        {
            var roles = await _repository.GetAllAsync();
            return _mapper.Map<List<RoleResDto>>(roles);
        }

        public async Task<RoleResDto?> GetByIdAsync(int Id)
        {
            var role = await _repository.GetByIdAsync(Id);
            if (role == null)
                throw new NotFoundException("Role Not Found");
            return _mapper.Map<RoleResDto>(role);
        }

        public async Task<RoleResDto> CreateAsync(RoleReqDto request)
        {
            var name = request.Name.Trim();
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Role name is Required");
            }
            var exists = await _repository.ExistsByNameAsync(name);
            if (exists)
            {
                throw new InvalidOperationException("Role name already exists");
            }
            var role = _mapper.Map<ApplicationRole>(request);
            await _repository.AddAsync(role);
            await _repository.SaveChangesAsync();
            return _mapper.Map<RoleResDto>(role);
        }

        public async Task<RoleResDto?> UpdateAsync(int Id, RoleReqDto request)
        {
            var role = await _repository.GetByIdAsync(Id);
            if (role == null)
                throw new NotFoundException("Role Not Found");
            var name = request.Name.Trim();
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Role name is Required");
            }
            var exists = await _repository.ExistsByNameAsync(name, Id);
            if (exists)
            {
                throw new InvalidOperationException("Role name already exists");
            }
                  _mapper.Map(request, role);

            await _repository.Update(role);
            await _repository.SaveChangesAsync();
            return _mapper.Map<RoleResDto>(role);

        } 
        public async Task<bool> DeleteAsync(int Id)
        {
            var role = await _repository.GetByIdAsync(Id);
            if (role == null)
                throw new NotFoundException("Role not Found");
            var hasUsers = await _repository.HasUsersAsync(Id);
            if (hasUsers)
            {
                throw new InvalidOperationException("Cannot delete this role because it is assigned to one or more users.");
            } 
            _repository.Delete(role);
            await _repository.SaveChangesAsync();
            return true;
        }

    }
}
