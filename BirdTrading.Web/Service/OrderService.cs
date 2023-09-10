using BirdTrading.Web.Models;
using BirdTrading.Web.Service.IService;
using BirdTrading.Web.Utility;

namespace BirdTrading.Web.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBaseService _baseService;
        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDTO?> CreateOrder(CartDTO cartDto)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.OrderAPIBase + "/api/order/CreateOrder"
            });
        }
    }
}
