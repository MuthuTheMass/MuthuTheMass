using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;

namespace CarParkingBooking.Services_Program
{
    public static class AppSettingValues
    {
        public static IConfiguration? configuration;
        public static string? JwtIssuer { get; set; } 
        public static string? JwtAudience { get; set; } 
        public static string? JwtSecretKey { get; set; } 
        public static string? JwtSqlConnection { get; set; } 
        
        public static string? JwtCosmosConnection { get; set; } 
        //public static string? AuthSqlConnection { get; set; }

        public static void Initialize(IConfiguration _configuration)
        {
            configuration = _configuration;
            JwtIssuer = configuration?["Authentication:Issuer"]!;
            JwtAudience = configuration?["Authentication:Audience"]!;
            JwtSecretKey = configuration?["Authentication:SecretKey"]!;
            JwtSqlConnection = configuration?.GetConnectionString("MyDbConnection")!;
            JwtCosmosConnection = configuration?.GetConnectionString("CosmosDb")!;
            //AuthSqlConnection = configuration?.GetConnectionString("AuthDbConnection")!;
        }

        
    }
}
