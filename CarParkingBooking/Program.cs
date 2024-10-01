using AutoMapper;
using CarParkingBooking.AutoMapper;
using CarParkingBooking.ExceptionHandler;
using CarParkingBookingDatabase.BookingDBContext;
using Microsoft.EntityFrameworkCore;
using CarParkingBooking.Services_Program;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.SeperateServicies();

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
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]!))
    };
});
GenerateJWTToken.Initialize(builder.Configuration);

builder.Services.AddDbContext<CarParkingBookingDBContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MyDbConnection"))
    );

builder.Services.AddScoped(serviceProvider => new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapperProfile());
}).CreateMapper());

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

app.MapControllers();

app.Run();
