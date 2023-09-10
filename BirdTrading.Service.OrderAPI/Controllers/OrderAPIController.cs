using AutoMapper;
using BirdTrading.Service.OrderAPI.Models;
using BirdTrading.Service.OrderAPI.Models.DTO;
using BirdTrading.Service.OrderAPI.Service.IService;
using BirdTrading.Service.OrderAPI.Utility;
using BirdTrading.Services.OrderAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdTrading.Service.OrderAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderAPIController : ControllerBase
    {
        protected ResponseDTO _response;
        private IMapper _mapper;
        private readonly AppDbContext _db;
        private IProductService _productService;


        public OrderAPIController(IProductService productService, IMapper mapper, AppDbContext db)
        {
            _db = db;   
            this._response = new ResponseDTO(); 
            _mapper = mapper;   
            _productService = productService;   
        }

        //[Authorize]
        [HttpPost("CreateOrder")]
        public async Task<ResponseDTO> CreateOrder([FromBody] CartDTO cartDto)
        {
            try
            {
                OrderHeaderDTO orderHeaderDto = _mapper.Map<OrderHeaderDTO>(cartDto.CartHeader);
                orderHeaderDto.OrderTime = DateTime.Now;
                orderHeaderDto.Status = SD.Status_Pending;
                orderHeaderDto.OrderDetails = _mapper.Map<IEnumerable<OrderDetailsDTO>>(cartDto.CartDetails);
                OrderHeader orderCreated = _db.OrderHeader.Add(_mapper.Map<OrderHeader>(orderHeaderDto)).Entity;
                await _db.SaveChangesAsync();

                orderHeaderDto.OrderHeaderId = orderCreated.OrderHeaderId;
                _response.Result = orderHeaderDto;
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
