namespace HovyFridge.Services.Auth
{
    public class JwtTokensServiceConfiguration
    {
        public TimeSpan JwtDefaultLifetime { get; set; } = TimeSpan.FromMinutes(10);
        public string JwtSecret { get; set; } = "B?E(H+MbQeThWmZq";
    }
}