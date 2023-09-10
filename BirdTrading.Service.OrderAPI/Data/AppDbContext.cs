using BirdTrading.Service.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BirdTrading.Services.OrderAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<OrderHeader> OrderHeader { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; } 


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);           
        }
    }
}