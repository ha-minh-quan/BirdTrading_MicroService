using BirdTrading.Web.Models;

namespace BirdTrading.Web.Service.IService
{
    public interface IOrderService
    {
        Task<ResponseDTO?> CreateOrder(CartDTO cartDto);
    }
}
