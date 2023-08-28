using BirdTrading.Web.Models;

namespace BirdTrading.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDto);
        Task<ResponseDTO?> RegisterAsync(RegisterationRequestDTO registerationRequestDTO);
        Task<ResponseDTO?> AssignRoleAsync(RegisterationRequestDTO registerationRequestDTO);
    }
}
