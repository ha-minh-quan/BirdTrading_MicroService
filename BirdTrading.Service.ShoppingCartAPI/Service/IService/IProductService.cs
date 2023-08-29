using BirdTrading.Service.ShoppingCartAPI.Models.DTO;

namespace BirdTrading.Service.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
    }
}
