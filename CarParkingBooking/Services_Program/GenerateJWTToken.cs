using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarParkingBooking.Services_Program
{
    public static class GenerateJWTToken
    {
        private static IConfiguration? configuration;
        public static void Initialize(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public static string GenerateJwtToken(string username, List<string> roles)
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
