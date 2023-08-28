using BirdTrading.Web.Models;
using BirdTrading.Web.Service.IService;
using BirdTrading.Web.Utility;
using ProductTrading.Web.Service.IService;

namespace BirdTrading.Web.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;
        public ProductService(IBaseService baseService) 
        {
            _baseService = baseService;
        }
        public async Task<ResponseDTO?> DeleteProduct(int id)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ProductAPIBase + "/api/product/" + id
            });
        }

        public async Task<ResponseDTO?> GetAllProductAsync()
        {
            return await _baseService.SendAsync(new RequestDTO()
            { 
             ApiType = SD.ApiType.GET,
             Url = SD.ProductAPIBase + "/api/product"
            });
        }

        public async Task<ResponseDTO?> GetProduct(int id)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product/" + id
			});
        }

        public async Task<ResponseDTO?> GetProductByName(string name)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ProductAPIBase + "/api/product/GetProductByName/"+name
            });
        }

        public async Task<ResponseDTO?> UpdateProduct(ProductDTO productDTO)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiType = SD.ApiType.PUT,
                Data = productDTO,
                Url = SD.ProductAPIBase + "/api/product"
            });
        }
        public async Task<ResponseDTO?> AddNewProduct(ProductDTO productDTO)
        {
			return await _baseService.SendAsync(new RequestDTO()
			{
				ApiType = SD.ApiType.POST,
                Data = productDTO,
				Url = SD.ProductAPIBase + "/api/product"
			});

		}
    }
}
