using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarParkingSystem.Application.Configuration
{
    public static class AppSettingValues
    {
        public static IConfiguration? configuration;
        public static string? JwtIssuer { get; set; }
        public static string? JwtAudience { get; set; }
        public static string? JwtSecretKey { get; set; }
        public static string? JwtSqlConnection { get; set; }

        public static string? CosmosConnection { get; set; }
        //public static string? AuthSqlConnection { get; set; }

        public static void Initialize(IConfiguration _configuration)
        {
            configuration = _configuration;
            JwtIssuer = configuration?["Authentication:Issuer"]!;
            JwtAudience = configuration?["Authentication:Audience"]!;
            JwtSecretKey = configuration?["Authentication:SecretKey"]!;
            JwtSqlConnection = configuration?.GetConnectionString("MyDbConnection")!;
            CosmosConnection = configuration?.GetConnectionString("CosmosDb")!;
            //AuthSqlConnection = configuration?.GetConnectionString("AuthDbConnection")!;
        }

        public static IServiceCollection AddCosmosClient(this IServiceCollection services, IConfiguration configuration)
        {
            // Read the Primary Connection String from the configuration
            if (string.IsNullOrWhiteSpace(CosmosConnection))
            {
                throw new ArgumentException(
                    "CosmosDb:PrimaryConnectionString is not configured properly in appsettings.json");
            }

            // Register the CosmosClient as a singleton service
            services.AddSingleton(serviceProvider => new CosmosClient(CosmosConnection));

            return services;
        }
    }
}