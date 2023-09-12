
using BirdTrading.Service.RewardAPI.Message;

namespace BirdTrading.Service.RewardAPI.Service
{
    public interface IRewardService
    {
        Task UpdateRewards(RewardsMessage rewardsMessage);
    }
}
