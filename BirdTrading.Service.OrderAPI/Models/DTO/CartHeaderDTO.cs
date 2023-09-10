using System.ComponentModel.DataAnnotations;

namespace BirdTrading.Service.OrderAPI.Models.DTO
{
    public class CartHeaderDTO
    {
        public int CartHeaderId { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public double Discount { get; set; }
        public double CartTotal { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? Email { get; set; }
    }
}
