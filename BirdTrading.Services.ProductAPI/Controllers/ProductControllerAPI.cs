using AutoMapper;
using Azure;
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
        public ResponseDTO AddNewProduct([FromForm] ProductDTO ProductDTO)
        {
            try
            {
                Product product = _mapper.Map<Product>(ProductDTO);
                _db.Products.Add(product);
                _db.SaveChanges();

                if (ProductDTO.Image != null)
                {

                    string fileName = product.ProductId + Path.GetExtension(ProductDTO.Image.FileName);
                    string filePath = @"wwwroot\ProductImages\" + fileName;
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        ProductDTO.Image.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    product.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                    product.ImageLocalPath = filePath;
                }
                else
                {
                    product.ImageUrl = "https://placehold.co/600x400";
                }
                _db.Products.Update(product);
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
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDTO UpdateProduct([FromForm] ProductDTO ProductDTO)
        {
            try
            {
                Product product = _mapper.Map<Product>(ProductDTO);

                if (ProductDTO.Image != null)
                {

                    if (!string.IsNullOrEmpty(product.ImageLocalPath))
                    {
                        var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), product.ImageLocalPath);
                        FileInfo file = new FileInfo(oldFilePathDirectory);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }

                    string fileName = product.ProductId + Path.GetExtension(ProductDTO.Image.FileName);
                    string filePath = @"wwwroot\ProductImages\" + fileName;
                    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                    {
                        ProductDTO.Image.CopyTo(fileStream);
                    }
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    product.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                    product.ImageLocalPath = filePath;
                }
                    
                _db.Products.Update(product);
                _db.SaveChanges();

                _responseDTO.Result = _mapper.Map<ProductDTO>(product);  
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
        public ResponseDTO DeleteProduct(int id) 
        {
            try
            {
                Product product = _db.Products.First(b => b.ProductId == id);

                if (!string.IsNullOrEmpty(product.ImageLocalPath))
                {
                    var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), product.ImageLocalPath);
                    FileInfo file = new FileInfo(oldFilePathDirectory);
                    if (file.Exists)
                    {
                        file.Delete();  
                    }
                }

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
