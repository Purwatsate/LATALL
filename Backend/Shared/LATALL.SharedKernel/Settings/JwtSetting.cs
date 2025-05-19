
namespace LATALL.SharedKernel.Settings
{
    public class JwtSetting
    {
        public required string Secret { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public int TokenExpiryInMinutes { get; set; }
    }
}
