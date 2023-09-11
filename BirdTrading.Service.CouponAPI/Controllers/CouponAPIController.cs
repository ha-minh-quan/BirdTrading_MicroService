using AutoMapper;
using BirdTrading.Service.CouponAPI.Models.DTO;
using BirdTrading.Service.CouponAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BirdTrading.Service.CouponAPI.Data;
using Azure;

namespace BirdTrading.Service.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    //[Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDTO _response;
        private IMapper _mapper;

        public CouponAPIController(AppDbContext DB, IMapper mapper)
        {
            _db = DB;
            _response = new ResponseDTO();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDTO>>(objList);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;

        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDTO Get(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(o => o.CouponId == id);
                _response.Result = _mapper.Map<CouponDTO>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;

        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDTO GetBycode(string code)
        {
            try
            {
                Coupon obj = _db.Coupons.First(o => o.CouponCode.ToLower() == code.ToLower());
                _response.Result = _mapper.Map<CouponDTO>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;

        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO Post([FromBody] CouponDTO couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(obj);
                _db.SaveChanges();



                var options = new Stripe.CouponCreateOptions
                {
                    AmountOff = (long)(couponDto.DiscountAmount * 100),
                    Name = couponDto.CouponCode,
                    Currency = "usd",
                    Id = couponDto.CouponCode,
                };
                var service = new Stripe.CouponService();
                service.Create(options);


                _response.Result = _mapper.Map<CouponDTO>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO update([FromBody] CouponDTO CouponDTO)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(CouponDTO);
                _db.Coupons.Update(obj);
                _db.SaveChanges();

                _response.Result = obj;
                _response.Message = "Update successfully!!!";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;

        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO Delete(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(u => u.CouponId == id);
                _db.Coupons.Remove(obj);
                _db.SaveChanges();


                var service = new Stripe.CouponService();
                service.Delete(obj.CouponCode);


            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

    }
}

