using BirdTrading.Service.EmailAPI.Models.DTO;

namespace BirdTrading.Service.EmailAPI.Service
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDTO cartDTO);
    }
}
