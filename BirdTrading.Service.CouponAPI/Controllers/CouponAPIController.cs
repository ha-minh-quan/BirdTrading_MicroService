using AutoMapper;
using BirdTrading.Service.CouponAPI.Models.DTO;
using BirdTrading.Service.CouponAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BirdTrading.Service.CouponAPI.Data;

namespace BirdTrading.Service.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
  ///  [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDTO _respone;
        private IMapper _mapper;

        public CouponAPIController(AppDbContext DB, IMapper mapper)
        {
            _db = DB;
            _respone = new ResponseDTO();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                _respone.Result = _mapper.Map<IEnumerable<CouponDTO>>(objList);

            }
            catch (Exception ex)
            {
                _respone.IsSuccess = false;
                _respone.Message = ex.Message;
            }
            return _respone;

        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDTO Get(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(o => o.CouponId == id);
                _respone.Result = _mapper.Map<CouponDTO>(obj);
            }
            catch (Exception ex)
            {
                _respone.IsSuccess = false;
                _respone.Message = ex.Message;

            }
            return _respone;

        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDTO GetBycode(String code)
        {
            try
            {
                Coupon obj = _db.Coupons.First(o => o.CouponCode.ToLower() == code.ToLower());
                _respone.Result = _mapper.Map<CouponDTO>(obj);
            }
            catch (Exception ex)
            {
                _respone.IsSuccess = false;
                _respone.Message = ex.Message;

            }
            return _respone;

        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO Create([FromBody] CouponDTO CouponDTO)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(CouponDTO);
                _db.Coupons.Add(obj);
                _db.SaveChanges();

                _respone.Result = obj;
                _respone.Message = "Create successfully!!!";
            }
            catch (Exception ex)
            {
                _respone.IsSuccess = false;
                _respone.Message = ex.Message;

            }
            return _respone;

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

                _respone.Result = obj;
                _respone.Message = "Update successfully!!!";
            }
            catch (Exception ex)
            {
                _respone.IsSuccess = false;
                _respone.Message = ex.Message;

            }
            return _respone;

        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO Delete(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(o => o.CouponId == id);
                _respone.Result = _mapper.Map<CouponDTO>(obj);

                _db.Coupons.Remove(obj);
                _db.SaveChanges();
                _respone.Message = "Delete successfully";
            }
            catch (Exception ex)
            {
                _respone.IsSuccess = false;
                _respone.Message = ex.Message;

            }
            return _respone;

        }

    }
}

