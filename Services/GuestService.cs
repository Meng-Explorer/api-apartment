using APARTMENT_API.DTOs.Request;
using APARTMENT_API.DTOs.Response;
using APARTMENT_API.Exceptions;



using APARTMENT_API.Helpers;
using APARTMENT_API.Models;
using APARTMENT_API.Repositories.Interfaces;
using APARTMENT_API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace APARTMENT_API.Services
{
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _repository;
        private IMapper _mapper;

        public GuestService (IGuestRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<PageResult<GuestResDto>> GetGuestByPage(int page = 1 , int pageSize = 10)
        {
            var guests = await _repository.GetGuestByPage(page, pageSize);
            if(guests == null)
            {
                throw new NotFoundException("Guest not found");
            }
            return new PageResult<GuestResDto>
            {
                PageNumber = guests.PageNumber,
                PageSize = guests.PageSize,
                TotalRecords = guests.TotalRecords,
                TotalPages = guests.TotalPages,
                Data = _mapper.Map<List<GuestResDto>>(guests.Data)
            };

        }

        public async Task<List<GuestResDto>> GetAllGuestAsync()
        {
            var data = await _repository.GatAllGuestAsync();
            return _mapper.Map<List<GuestResDto>>(data);
        }

        public async Task<GuestResDto> GetGuestByIdAsync(int guestId)
        {
            var data = await _repository.GetGuestByIdAsync(guestId);
            return _mapper.Map<GuestResDto>(data);
        }

        public async Task<GuestResDto> CreateGuestAsync(GuestReqDto reqDto)
        {
            if(reqDto == null)
            {
                throw new BadRequestException("Bad Request");
            }
            if(reqDto.Image == null || reqDto.Image.Length == 0)
            {
                throw new BadRequestException("Please select an Image");
            }

            var allowedExtension = new[]
            {
                ".jpg",
                ".jpeg",
                ".png",
                ".webp"
            };

            var extension = Path.GetExtension(reqDto.Image.FileName).ToLowerInvariant();
            if (!allowedExtension.Contains(extension))
            {
                throw new BadRequestException("Only JPG, JPEG, PNG and WEBP files are Allow");
            }

            const long maxFilesSize = 5 * 1024 * 1024;
            if(reqDto.Image.Length > maxFilesSize)
            {
                throw new BadRequestException("Image size cannot exceed 5MB");
            }

            var filesName = $"{Guid.NewGuid()}{extension}";
            var ImagePath = Path.Combine("uploads", "Images-guest");
            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot",ImagePath
            );
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var filePath = Path.Combine(uploadsFolder, filesName);
            await using ( var stream  = new FileStream(filePath, FileMode.Create))
            {
                await reqDto.Image.CopyToAsync(stream);

            }
            var newData = _mapper.Map<GuestReqDto, Guest>(reqDto);
            newData.ImagePath = Path.Combine(ImagePath, filesName);
            var result = await _repository.CreateGuestAsync(newData);
            return _mapper.Map<Guest, GuestResDto>(result);
                

        }
        public async Task<GuestResDto> UpdateGuestAsync(int questId,GuestReqDto reqDto)
        {
            if(reqDto is null)
            {
                throw new BadRequestException("Bad Request");
            }
            var existingGuest = await _repository.GetGuestByIdAsync(questId);
            if(existingGuest is null)
            {
                throw new NotFoundException("Guest not Found");
            }
            var oldImagePath = existingGuest.ImagePath;
            _mapper.Map(reqDto, existingGuest);
            if(reqDto.Image is not null && reqDto.Image.Length > 0)
            {
                var allowedExtensions = new[]
                {
                    ".jpg",
                    ".jpeg",
                    ".png",
                    ".webp"
                };
                var extension = Path.GetExtension(reqDto.Image.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    throw new BadRequestException(" Only JPG, PNG ,JPEG and WEBP files are allowed");
                }
                const long maxFilesSize = 5 * 1024 * 1024;
                if(reqDto.Image.Length > maxFilesSize)
                {
                    throw new BadRequestException("Image size cannot exceed 5MB");
                }
                var imagePath = Path.Combine("uploads", "images-guest");
                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot", imagePath
                );
                Directory.CreateDirectory(uploadsFolder);
                var fileName = $"{Guid.NewGuid()}{extension}";
                var newFilesPath = Path.Combine(uploadsFolder, fileName);

                await using (var stream = new FileStream(newFilesPath, FileMode.Create))
                {
                    await reqDto.Image.CopyToAsync(stream);
                }
                existingGuest.ImagePath = Path.Combine(imagePath, fileName);

            }
            else
            {
                existingGuest.ImagePath = oldImagePath;
            }

            var result = await _repository.UpdateGuestAsync(existingGuest);
            return _mapper.Map<Guest, GuestResDto>(result);

        }
        public async Task<bool> DeleteGuestAsync(int guestId)
        {
            var entity = await _repository.GetGuestByIdAsync(guestId);
            if (entity == null)
                throw new NotFoundException("Not Found");
            await _repository.DeleteGuestAsync(entity);
            return true;
        }







    }
}
