using AutoMapper;
using CarParkingBooking.AutoMapper;
using CarParkingBooking.ExceptionHandler;
using CarParkingBookingDatabase.BookingDBContext;
using Microsoft.EntityFrameworkCore;
using ValidateCarParkingDetails.ValidateAuthorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IAuthorization, Authorization>();
builder.Services.AddTransient<IDealerData, DealerData>();

builder.Services.AddDbContext<CarParkingBookingDBContext>(opt => 
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MyDbConnection"))
    );

builder.Services.AddScoped(serviceProvider => new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapperProfile());
}).CreateMapper());

builder.Services.AddCors(options =>
{
    options.AddPolicy("carparkingorigins",
                     builder => builder.WithOrigins("http://localhost:4200")
                                       .AllowAnyOrigin()
                                       .AllowAnyHeader()
        );
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

app.UseAuthorization();

app.MapControllers();
app.UseCors("carparkingorigins");
app.Run();
