using BirdTrading.Service.OrderAPI.Models.DTO;

namespace BirdTrading.Service.OrderAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
    }
}
