namespace BirdTrading.Web.Models
{
    public class StripeRequestDTO
    {
        public string? StripeSessionUrl { get; set; }
        public string? StripeSessionId { get; set; }
        public string ApproveUrl { get; set; }
        public string CancelUrl { get; set; }
        public OrderHeaderDTO OrderHeader { get; set; }
    }
}
