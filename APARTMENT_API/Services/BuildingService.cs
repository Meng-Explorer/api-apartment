using APARTMENT_API.DTOs.Request;
using APARTMENT_API.DTOs.Response;
using APARTMENT_API.Exceptions;
using APARTMENT_API.Helpers;
using APARTMENT_API.Models;
using APARTMENT_API.Repositories.Interfaces;
using APARTMENT_API.Services.Interfaces;
using AutoMapper;
namespace APARTMENT_API.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository _repository;
        private readonly IMapper _mapper;

        public BuildingService(IBuildingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PageResult<BuildingResDto>> GetBuilding(int page = 1, int pageSize = 10)
        {
            var buildings = await _repository.GetBuilding(page, pageSize);
            if (buildings == null)
            {
                throw new NotFoundException("Data not found:");
            }
            return new PageResult<BuildingResDto>
            {
                PageNumber = buildings.PageNumber,
                PageSize = buildings.PageSize,
                TotalRecords = buildings.TotalRecords,
                TotalPages = buildings.TotalPages,
                Data = _mapper.Map<List<BuildingResDto>>(buildings.Data)
            };
        }
        public async Task<List<BuildingResDto>> GetAllBuild()
        {
            var floor = await _repository.GetAllBuild();
            return _mapper.Map<List<BuildingResDto>>(floor);
        }
        public async Task<BuildingResDto> GetBuilding(int id)
        {
            if (id == 0)
            {
                throw new ValidationException(
                    new List<string> { "Id must be greater than 0" }
              );
            }
            var building = await _repository.GetBuilding(id);
            if (building == null)
            {
                throw new NotFoundException("Data not found:");
            }
            return _mapper.Map<Building, BuildingResDto>(building);

        }
        public async Task<BuildingResDto> Create(BuildingReqDto buildingDto)
        {
             var Errors = new List<string>();
            if (string.IsNullOrEmpty(buildingDto.NameKhmer))
            {
                Errors.Add("Name khmer is required");
            }
            if (string.IsNullOrEmpty(buildingDto.NameEnglish))
            {
                Errors.Add("Name english is required");
            }
            if (Errors.Count > 0)
            {
                throw new ValidationException(Errors);
            }
            var data = _mapper.Map<Building>(buildingDto);
            var building = await _repository.Create(data);
            return _mapper.Map<Building, BuildingResDto>(building);

        }

        public async Task<BuildingResDto> Update(int id, BuildingReqDto buildingDto)
        {
            var entity = await _repository.GetBuilding(id);
            if (entity == null)
                throw new NotFoundException("Not found:");
            var Errors = new List<string>();
            if (string.IsNullOrEmpty(buildingDto.NameKhmer))
            {
                Errors.Add("Name khmer is required");
            }
            if (string.IsNullOrEmpty(buildingDto.NameEnglish))
            {
                Errors.Add("Name english is required");
            }
            if (Errors.Count > 0)
            {
                throw new ValidationException(Errors);
            }
            _mapper.Map(buildingDto, entity);
            var building = await _repository.Update(entity);
            return _mapper.Map<Building, BuildingResDto>(building);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _repository.GetBuilding(id);
            if (entity == null)
                throw new NotFoundException("Not found:");
            await _repository.Delete(entity);
            return true;
        }

        
    }

}