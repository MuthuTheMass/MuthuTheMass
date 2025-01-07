using CarParkingSystem.Infrastructure.Configurations;

namespace CarParkingSystem.Domain.Entities;

public class UserInformation
{

    public required string id { get; set; } = string.Empty;
    public required string UserID { get; set; } = string.Empty;

    public required string Name { get; set; }

    public string? UserProfilePicture { get; set; }


    public required string Email { get; set; }

    public required string MobileNumber { get; set; }

    public required string Password { get; set; }


    public string Rights { get; set; } = ""; //AccessToUsers.User.ToString();

    public string? Address { get; set; }

    public DateTime? CreatedDate { get; set; } = DateTiming.GetIndianTime();
}