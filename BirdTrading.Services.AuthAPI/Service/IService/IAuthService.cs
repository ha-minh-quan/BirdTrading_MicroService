using BirdTrading.Services.AuthAPI.Models.Dto;

namespace BirdTrading.Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegisterationRequestDTO registerationRequestDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<bool> AssignRole(string email, string roleName);
    }
}