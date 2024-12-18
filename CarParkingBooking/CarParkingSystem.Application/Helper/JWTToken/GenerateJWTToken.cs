using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarParkingBooking.Services_Program;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CarParkingSystem.Application.Helper.JWTToken
{
    public static class GenerateJwtToken
    {
        private static IConfiguration? configuration;
        public static void Initialize(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public static string GenerateJwtTokenToAuthorize(string username, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingValues.JwtSecretKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: AppSettingValues.JwtIssuer,
                audience: AppSettingValues.JwtAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
