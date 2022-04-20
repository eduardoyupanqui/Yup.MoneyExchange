using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Yup.MoneyExchange.Api.Configs;

namespace Yup.MoneyExchange.Api.Helpers
{
    public class TokenGenerator
    {
        private readonly AuthConfig _authConfig;
        public TokenGenerator(IOptions<AuthConfig> authConfigOptions)
        {
            _authConfig = authConfigOptions.Value;
        }
        public string GenerateJwtToken(string accountId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", accountId.ToString()) }),
                Issuer = _authConfig.Issuer,
                Audience = _authConfig.Audience,
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
