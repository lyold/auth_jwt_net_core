
namespace AuthJWT.Domain.Services.Security
{
    public class TokenConfigurationcs
    {
        public string Audience { get; set; }

        public string Issuer { get; set; }

        public int TimeSession { get; set; }
    }
}
