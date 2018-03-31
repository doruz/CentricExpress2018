using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JwtSample
{
    public class JwtOptions
    {
        public TimeSpan ValidFor { get; set; }

        public string SecurityKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public DateTime IssuedAt => DateTime.UtcNow;

        public DateTime ExpiresAt => DateTime.UtcNow.Add(ValidFor);

        public SymmetricSecurityKey IssuerSigningKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));

        public SigningCredentials SigningCredentials => new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256);
    }
}
