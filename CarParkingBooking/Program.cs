using AutoMapper;
using CarParkingBooking.AutoMapper;
using CarParkingBooking.ExceptionHandler;
using CarParkingBooking.Services_Program;
using CarParkingBookingDatabase.BookingDBContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
GenerateJWTToken.Initialize(builder.Configuration);
AppSettingValues.Initialize(builder.Configuration);


builder.Services.SeperateServicies();

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

builder.Services.AddDbContext<CarParkingBookingDBContext>(opt =>
    opt.UseSqlServer(AppSettingValues.JwtSqlConnection)
    );

//builder.Services.AddDbContext<AuthDbContext>(opt =>
//    opt.UseSqlServer(AppSettingValues.AuthSqlConnection)
//    );

//builder.Services.AddIdentity<UserDetails, IdentityRole<int>>()
//    .AddEntityFrameworkStores<CarParkingBookingDBContext>()
//    .AddDefaultTokenProviders();

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


