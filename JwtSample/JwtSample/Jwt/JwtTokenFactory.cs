using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JwtSample
{
    public sealed class JwtTokenFactory
    {
        private readonly JwtOptions jwtOptions;

        public JwtTokenFactory(JwtOptions jwtOptions)
        {
            this.jwtOptions = jwtOptions;
        }

        public string CreateToken(User user)
        {
            EnsureUserIsNotNull(user);

            var jwtToken = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                notBefore: jwtOptions.IssuedAt,
                expires: jwtOptions.ExpiresAt,
                signingCredentials: jwtOptions.SigningCredentials,
                claims: GetUserClaims(user));

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        private IEnumerable<Claim> GetUserClaims(User user)
        {
            yield return new Claim(ClaimTypes.Name, user.UserName);
            yield return new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString());
            yield return new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());

            yield return new Claim(JwtRegisteredClaimNames.Iat,
                new DateTimeOffset(jwtOptions.IssuedAt).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64);
        }

        private void EnsureUserIsNotNull(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
        }
    }
}
