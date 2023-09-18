using BirdTrading.Web.Models;
using BirdTrading.Web.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductTrading.Web.Service.IService;
using System.Collections.Generic;

namespace BirdTrading.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService) {
            _productService = productService; 
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDTO> list = new();

            ResponseDTO? response = await _productService.GetAllProductAsync();
            if (response != null && response.IsSuccess) 
            {
                list = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDTO product)
        {
			if (ModelState.IsValid)
			{
				ResponseDTO? response = await _productService.AddNewProduct(product);

				if (response != null && response.IsSuccess)
				{
                    TempData["success"] = "Product created successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
        }
			return View(product);
		}
        public async Task<IActionResult> ProductDelete(int productId)
        {
            ResponseDTO? response = await _productService.GetProduct(productId);

            if (response != null && response.IsSuccess)
            {
                ProductDTO? model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDTO product)
        {
            ResponseDTO? response = await _productService.DeleteProduct(product.ProductId);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product deleted successfully";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(product);
        }
        public async Task<IActionResult> ProductEdit(int productId)
        {
            ResponseDTO? response = await _productService.GetProduct(productId);

            if (response != null && response.IsSuccess)
            {
                ProductDTO? model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                ResponseDTO? response = await _productService.UpdateProduct(product);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Product updated successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(product);
        }
    } 
}
