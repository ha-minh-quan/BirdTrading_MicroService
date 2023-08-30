using BirdTrading.Web.Service;
using BirdTrading.Web.Service.IService;
using BirdTrading.Web.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProductTrading.Web.Service.IService;

namespace BirdTrading.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();
            builder.Services.AddHttpClient<IProductService, ProductService>();
            builder.Services.AddHttpClient<ICouponService, CouponService>();
            builder.Services.AddHttpClient<IAuthService, AuthService>();
            builder.Services.AddHttpClient<ICartService, CartService>();

            SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];
            SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];
            SD.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];
            SD.ShoppingCartAPIBase = builder.Configuration["ServiceUrls:ShoppingCartAPI"];

            builder.Services.AddScoped<ITokenProvider, TokenProvider>();
            builder.Services.AddScoped<IBaseService, BaseService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICouponService, CouponService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromHours(10);
                    options.LoginPath = "/Auth/Logim";
                    options.AccessDeniedPath = "/Auth/AccessDenied";
                }
                );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}