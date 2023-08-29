using BirdTrading.Service.ShoppingCartAPI.Models.DTO;

namespace BirdTrading.Service.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDTO> GetCoupon(string couponCode);
    }
}
