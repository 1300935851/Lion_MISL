using KAIFA_Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KAIFA_Api.BLL
{
    public class JWTAuthenticationHelper
    {
        private readonly IConfiguration configuration;

        public JWTAuthenticationHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string BuildToken(MeterInfo meterInfo)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Expiration, ""),
                new Claim("MeterNumber", meterInfo.MeterNumber),
                new Claim("IsSubmeter", meterInfo.IsSubmeter.ToString()),
                new Claim("IP", meterInfo.IP),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Authentication:Issuer"],
                audience: configuration["Authentication:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                notBefore: DateTime.Now,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public MeterInfo? ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(configuration["Authentication:SecretKey"]!);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Authentication:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Authentication:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);

                MeterInfo meterInfo = new MeterInfo();
                meterInfo.MeterNumber = claimsPrincipal.FindFirstValue("MeterNumber") ?? "";
                meterInfo.IP = claimsPrincipal.FindFirstValue("IP") ?? "";
                meterInfo.IsSubmeter = Convert.ToBoolean(claimsPrincipal.FindFirstValue("IsSubmeter") ?? "false");

                return meterInfo;
            }
            catch
            {
                return null;
            }
        }
    }
}
