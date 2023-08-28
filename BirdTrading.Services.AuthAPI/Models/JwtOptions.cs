namespace BirdTrading.Services.AuthAPI.Models
{
    public class JwtOptions
    {
        public String Issuer { get; set; } = string.Empty;
        public String Audience { get; set; } = string.Empty;
        public String Secret { get; set; } = string.Empty;


    }
}
