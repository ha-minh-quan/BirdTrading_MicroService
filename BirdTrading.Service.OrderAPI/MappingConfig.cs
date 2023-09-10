using AutoMapper;
using BirdTrading.Service.OrderAPI.Models;
using BirdTrading.Service.OrderAPI.Models.DTO;

namespace BirdTrading.Services.OrderAPI;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps() {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<OrderHeaderDTO, CartHeaderDTO>()
            .ForMember(dest => dest.CartTotal, u => u.MapFrom(src => src.OrderTotal)).ReverseMap();

            config.CreateMap<CartDetailsDTO, OrderDetailsDTO>()
            .ForMember(dest => dest.ProductName, u => u.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Price, u => u.MapFrom(src => src.Product.Price));
            config.CreateMap<OrderDetailsDTO, CartDetailsDTO>();

            config.CreateMap<OrderHeader, OrderHeaderDTO>().ReverseMap();
            config.CreateMap<OrderDetailsDTO, OrderDetails>().ReverseMap();

        });
        return mappingConfig;
    }
}       

