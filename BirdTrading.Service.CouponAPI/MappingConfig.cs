using AutoMapper;
using BirdTrading.Service.CouponAPI.Models;
using BirdTrading.Service.CouponAPI.Models.DTO;


namespace BirdTrading.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps() {
            var mappingCongif = new MapperConfiguration(config => {
                config.CreateMap<CouponDTO, Coupon>();
                config.CreateMap<Coupon, CouponDTO>();
            });
            return mappingCongif;
        }       
    }
}
