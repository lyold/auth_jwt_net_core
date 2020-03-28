
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace AuthJWT.Domain.Services.Security
{
    public class SignConfiguration
    {
        public SecurityKey Key { get; }

        public SigningCredentials Credentials { get; }

        public SignConfiguration()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            Credentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
