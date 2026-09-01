using APARTMENT_API.DTOs.Request;
using APARTMENT_API.DTOs.Response;

namespace APARTMENT_API.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<AuthResDto?> Login(AuthReqDto req);
    }
}
