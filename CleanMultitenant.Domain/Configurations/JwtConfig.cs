namespace CleanMultitenant.Domain.Configurations
{
    public class JwtConfig
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationMinutes { get; set; }
        public string Secret { get; set; }
    }
}
