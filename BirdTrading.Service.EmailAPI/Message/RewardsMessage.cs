namespace BirdTrading.Service.EmailAPI.Message
{
    public class RewardsMessage
    {
        public string UserId { get; set; }
        public int RewardActivity { get; set; }
        public int OrderId { get; set; }
    }
}
