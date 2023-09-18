using BirdTrading.Web.Models;

namespace ProductTrading.Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDTO?> GetProduct(int id);
        Task<ResponseDTO?> GetAllProductAsync();
        Task<ResponseDTO?> UpdateProduct(ProductDTO ProductDTO);
        Task<ResponseDTO?> DeleteProduct(int id);
		Task<ResponseDTO?> AddNewProduct(ProductDTO ProductDTO);
	}
}
