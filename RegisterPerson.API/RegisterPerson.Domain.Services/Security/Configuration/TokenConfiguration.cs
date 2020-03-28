
namespace AuthJWT.Domain.Services.Security
{
    public class TokenConfiguration
    {
        public string Audience { get; set; }

        public string Issuer { get; set; }

        public int TimeSession { get; set; }
    }
}
