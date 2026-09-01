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
    public class FloorService :IFloorService
    {
        private readonly IFloorRepository _repository;
        private readonly IMapper _mapper;
        public FloorService(IFloorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<PageResult<FloorResDto>> GetFloorAsynce(int page = 1, int pageSize = 10)
        {
            var floor  = await _repository.GetFloorAsynce(page, pageSize);
            if (floor == null)
            {
                throw new NotFoundException("Data not found:");
            }
            return new PageResult<FloorResDto>
            {
                PageNumber = floor.PageNumber,
                PageSize = floor.PageSize,
                TotalRecords = floor.TotalRecords,
                TotalPages = floor.TotalPages,
                Data = _mapper.Map<List<FloorResDto>>(floor.Data)
            };
        }
        public async Task<List<FloorResDto>> GetAllFloorAsynce()
        {
            var floor = await _repository.GetAllFloorAsynce();
            return _mapper.Map<List<FloorResDto>>(floor);
        }
        public async Task<FloorResDto> GetFloorByIdAsynce(int floorId)
        {
            if (floorId == 0)
            {
                throw new ValidationException(
                    new List<string> { "Id must be greater than 0" }
              );
            }
            var floor = await _repository.GetFloorByIdAsynce(floorId);
            if (floor == null)
            {
                throw new NotFoundException("Data not found:");
            }
            return _mapper.Map<Floor,FloorResDto>(floor);

        }
        public async Task<FloorResDto> CreateAsynce(FloorReqDto floorReqDto)
        {
            if(floorReqDto == null)
            {
                throw new BadRequestException("Bad Request");
            }
            var newData = _mapper.Map<FloorReqDto, Floor>(floorReqDto);
            var floor = await _repository.CreateAsynce(newData);
            return _mapper.Map<FloorResDto>(floor);
        }
        public async Task<FloorResDto> UpdateAsynce(int floorId, FloorReqDto floorReqdto)
        {
            if(floorReqdto == null)
            {
                throw new BadRequestException("Bad Request");
            }
            var entity = await _repository.GetFloorByIdAsynce(floorId);
            if (entity == null)
            {
                throw new NotFoundException("Not Found!");
            }
            _mapper.Map(floorReqdto,entity);
            var floor = await _repository.UpdateAsynce(entity);
            return _mapper.Map<Floor, FloorResDto>(floor);
        }

        public async Task<bool> DeleteAsynce(int floorId)
        {
            var entity = await _repository.GetFloorByIdAsynce(floorId);
            if (entity == null)
                throw new NotFoundException("Not found:");
            await _repository.DeleteAsynce(entity);
            return true;
        }







    }
}
