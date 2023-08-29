﻿namespace BirdTrading.Service.ShoppingCartAPI.Models.DTO
{
    public class CartDTO
    {
        public CartHeaderDTO CartHeader { get; set; }  
        public IEnumerable<CartDetailsDTO>? CartDetails { get; set; }
    }
}
