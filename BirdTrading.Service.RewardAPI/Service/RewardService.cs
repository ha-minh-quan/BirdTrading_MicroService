﻿
using BirdTrading.Service.RewardAPI.Message;
using BirdTrading.Service.RewardAPI.Models;
using BirdTrading.Service.RewardAPI.Service;
using BirdTrading.Services.RewardAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BirdTrading.Service.EmailAPI.Service
{
    public class RewardService : IRewardService
    {
        private DbContextOptions<AppDbContext> _dbOptions;

        public RewardService(DbContextOptions<AppDbContext> dbOptions)
        {
            _dbOptions = dbOptions; 
        }

        public async Task UpdateRewards(RewardsMessage rewardsMessage)
        {
            try
            {
                Rewards rewards = new()
                {
                    OrderId = rewardsMessage.OrderId,
                    RewardsActivity = rewardsMessage.RewardActivity,
                    UserId = rewardsMessage.UserId,
                    RewardsDate = DateTime.Now  
                };

                await using var _db = new AppDbContext(_dbOptions);
                _db.Rewards.AddAsync(rewards);
                await _db.SaveChangesAsync();   

            } catch (Exception ex) {
            }
        }
    }
}
