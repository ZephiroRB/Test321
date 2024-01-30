namespace CleanMultitenant.Application.Services.Users.Login
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string Tenant { get; set; }
    }
}
