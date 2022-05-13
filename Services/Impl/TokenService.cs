using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using worksheet2.Model.Settings;

namespace worksheet2.Services.Impl
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings _settings;

        public TokenService(IOptions<AppSettings> options)
        {
            _settings = options.Value;
        }

        public bool ValidateToken(string token)
        {
            var userId = GetUserIdFromToken(token);

            return userId is {Length: > 0};
        }

        public string GetUserIdFromToken(string token)
        {
            try
            {
                var securityTokenHandler = new JwtSecurityTokenHandler();
                var encoding = Encoding.ASCII;
                var appSettingsSecret = _settings.Secret;
                var key = encoding.GetBytes(appSettingsSecret);
                var symmetricSecurityKey = new SymmetricSecurityKey(key);
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = symmetricSecurityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ClockSkew = TimeSpan.Zero
                };
                securityTokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                var jwtSecurityToken = (JwtSecurityToken) securityToken;

                var enumerable = jwtSecurityToken.Claims;
                return enumerable.First(x => x.Type == "id").Value;
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}