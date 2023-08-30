using BirdTrading.Web.Models;

namespace BirdTrading.Web.Service.IService
{
    public interface ICartService
    {
        Task<ResponseDTO?> GetCartByUserIdAsnyc(string userId);
        Task<ResponseDTO?> UpsertCartAsync(CartDTO cartDto);
        Task<ResponseDTO?> RemoveFromCartAsync(int cartDetailsId);
        Task<ResponseDTO?> ApplyCouponAsync(CartDTO cartDto);
    }
}
