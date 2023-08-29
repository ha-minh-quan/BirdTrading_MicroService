using AutoMapper;
using BirdTrading.Service.ShoppingCartAPI.Models;
using BirdTrading.Service.ShoppingCartAPI.Models.DTO;


namespace BirdTrading.Services.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps() {
            var mappingCongif = new MapperConfiguration(config => {
                config.CreateMap<CartHeader, CartHeaderDTO>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDTO>().ReverseMap();
            });
            return mappingCongif;
        }       
    }
}
