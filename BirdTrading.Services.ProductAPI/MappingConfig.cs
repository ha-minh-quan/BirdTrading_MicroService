using AutoMapper;
using BirdTrading.Services.ProductAPI.Models;
using BirdTrading.Services.ProductAPI.Models.DTO;

namespace BirdTrading.Services.BirdAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps() {
            var mappingCongif = new MapperConfiguration(config => {
                config.CreateMap<ProductDTO, Product>();
                config.CreateMap<Product, ProductDTO>();
            });
            return mappingCongif;
        }       
    }
}
