using AutoMapper;
using CarParkingBooking.AutoMapper;
using CarParkingBooking.ExceptionHandler;
using CarParkingBooking.Services_Program;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CarParkingSystem.Application.Helper.JWTToken;
using CarParkingSystem.Infrastructure.Database.SQLDatabase.BookingDBContext;
using Microsoft.Azure.Cosmos;
using CarParkingSystem.Application.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Car Parking Booking API",
        Version = "v1"
    });

    // Define JWT Bearer token authentication scheme
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter JWT Bearer token **_only_**. Example: `Bearer {your token}`"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

GenerateJwtToken.Initialize(builder.Configuration);
AppSettingValues.Initialize(builder.Configuration);
builder.Services.AddCosmosClient(builder.Configuration);


builder.Services.AddCors(options =>
{
    options.AddPolicy("carparkingorigins",
        builder => builder.WithOrigins("http://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = AppSettingValues.JwtIssuer,
        ValidAudience = AppSettingValues.JwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingValues.JwtSecretKey!))
    };
});

builder.Services.AddDbContext<CarParkingBookingDbContext>(opt =>
    opt.UseSqlServer(AppSettingValues.JwtSqlConnection)
    );

builder.Services.AddSingleton<CosmosClient>(provider => new CosmosClient(AppSettingValues.JwtCosmosConnection));

builder.Services.AddScoped(serviceProvider => new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapperProfile());
}).CreateMapper());

// Role seeding at startup
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));
});

builder.Services.SeperateServicies();

var app = builder.Build();

app.UseMiddleware<CommonExceptionHandler>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("carparkingorigins");
app.MapControllers();
app.Run();
