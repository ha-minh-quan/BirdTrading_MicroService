using AutoMapper;
using BirdTrading.Services.ProductAPI.Data;
using BirdTrading.Services.ProductAPI.Models;
using BirdTrading.Services.ProductAPI.Models.Dto;
using BirdTrading.Services.ProductAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BirdTrading.Services.BirdAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductControllerAPI : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDTO _responseDTO;
        private IMapper _mapper;
        public ProductControllerAPI(AppDbContext db, IMapper mapper) {
            _db = db;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
        }

        [HttpGet]
        public ResponseDTO GetAllProduct() {
            try
            {
                IEnumerable<Product> productList = _db.Products.ToList();
                _responseDTO.Result = _mapper.Map<IEnumerable<Product>>(productList);
            } catch (Exception ex) {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return _responseDTO;
        }
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDTO GetProduct(int id)
        {
            try {
                Product product = _db.Products.First(b => b.ProductId == id);
                _responseDTO.Result = _mapper.Map<ProductDTO>(product);
            } catch (Exception ex) {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return _responseDTO;
        }
        [HttpGet]
        [Route("GetProductByName/{name}")]
        public ResponseDTO GetProductByName(String name)
        {
            try
            {
                Product product = _db.Products.First(b => b.Name == name);
                _responseDTO.Result = _mapper.Map<ProductDTO>(product);
            }catch (Exception ex) {
                _responseDTO.IsSuccess = false;   
                _responseDTO.Message = ex.Message;  
            }
            return _responseDTO;    
        }
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO AddNewProduct([FromBody] ProductDTO ProductDTO)
        {
            try
            {
                Product obj = _mapper.Map<Product>(ProductDTO);
				_db.Products.Add(obj); 
				_db.SaveChanges();

				_responseDTO.Result = _mapper.Map<ProductDTO>(obj);
			}
			catch (Exception ex) {
                _responseDTO.IsSuccess=false;   
                _responseDTO.Message = ex.Message;  
            }
            return _responseDTO;
        }
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO UpdateProduct(ProductDTO product)
        {
            try
            {
                Product obj = _mapper.Map<Product>(product);
                _db.Products.Update(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _responseDTO.IsSuccess=false;   
                _responseDTO.Message = ex.Message;  
            }
            return _responseDTO;
        }
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO DeleteProduct(int id) {
            try
            {
                Product product = _db.Products.First(b => b.ProductId == id);
                _db.Remove(product);
                _db.SaveChanges();
                _responseDTO.Result = _mapper.Map<ProductDTO>(product);
            }
             catch (Exception ex)
            {
                _responseDTO.IsSuccess = false;
                _responseDTO.Message = ex.Message;
            }
            return _responseDTO; 
        }
    }
}
