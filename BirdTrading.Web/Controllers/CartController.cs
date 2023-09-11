using BirdTrading.Web.Models;
using BirdTrading.Web.Service;
using BirdTrading.Web.Service.IService;
using BirdTrading.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace BirdTrading.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        public CartController(ICartService cartService, IOrderService orderService  )
        {
            _cartService = cartService;
            _orderService = orderService;
        }
        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            return View(await LoadCartDtoBasedOnLoggedInUser());
        }

        [HttpPost]
        [ActionName("Checkout")]
        public async Task<IActionResult> Checkout(CartDTO cartDto)
        {

            CartDTO cart = await LoadCartDtoBasedOnLoggedInUser();
            cart.CartHeader.Phone = cartDto.CartHeader.Phone;
            cart.CartHeader.Email = cartDto.CartHeader.Email;
            cart.CartHeader.Name = cartDto.CartHeader.Name;

            var response = await _orderService.CreateOrder(cart);
            OrderHeaderDTO orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDTO>(Convert.ToString(response.Result));

            if (response != null && response.IsSuccess)
            {
                //get stripe session and redirect to stripe to place order
                var domain = Request.Scheme + "://" + Request.Host.Value + "/";

                StripeRequestDTO stripeRequestDTO = new()
                {
                    ApproveUrl = domain + "cart/Confirmation?orderId=" + orderHeaderDto.OrderHeaderId,
                    CancelUrl = domain + "cart/checkout",
                    OrderHeader = orderHeaderDto,
                };

                var stripeResponse = await _orderService.CreateStripeSession(stripeRequestDTO);
                StripeRequestDTO stripeResponseResult = JsonConvert.DeserializeObject<StripeRequestDTO>(Convert.ToString(stripeResponse.Result));
                Response.Headers.Add("Location", stripeResponseResult.StripeSessionUrl);
                return new StatusCodeResult(303);
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Confirmation(int orderId)
        {
            ResponseDTO? response = await _orderService.ValidateStripeSession(orderId);
            if (response != null & response.IsSuccess)
            {
                OrderHeaderDTO orderHeader = JsonConvert.DeserializeObject<OrderHeaderDTO>(Convert.ToString(response.Result));
                if (orderHeader.Status == SD.Status_Approved)
                {
                    return View(orderId);
                }
            }
            return View(orderId);
        }
        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDTO? response = await _cartService.RemoveFromCartAsync(cartDetailsId);
            if (response != null & response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CartDTO cartDTO)
        {
            ResponseDTO? response = await _cartService.ApplyCouponAsync(cartDTO);
            if (response != null & response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmailCart(CartDTO cartDTO)
        {
            CartDTO cart = await LoadCartDtoBasedOnLoggedInUser();
            cart.CartHeader.Email = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Email)?.FirstOrDefault()?.Value;
            ResponseDTO? response = await _cartService.EmailCart(cart);
            if (response != null & response.IsSuccess)
            {
                TempData["success"] = "Email will be processed and sent shortly";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDTO cartDTO)
        {
            cartDTO.CartHeader.CouponCode = "";
            ResponseDTO? response = await _cartService.ApplyCouponAsync(cartDTO);
            if (response != null & response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }
        
        private async Task<CartDTO> LoadCartDtoBasedOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDTO? response = await _cartService.GetCartByUserIdAsnyc(userId);
            if (response != null & response.IsSuccess)
            {
                CartDTO cartDto = JsonConvert.DeserializeObject<CartDTO>(Convert.ToString(response.Result));
                return cartDto;
            }
            return new CartDTO();
        }
    }
}
