﻿using CarParkingSystem.Domain.Helper;
using System.ComponentModel.DataAnnotations;

namespace CarParkingSystem.Domain.Entities.SQL
{
    public class UserDetails
    {
        [Key] public required string UserID { get; set; } = string.Empty;
        [DataType(DataType.Text)] public required string Name { get; set; }

        public byte[]? UserProfilePicture { get; set; }

        [DataType(DataType.EmailAddress)] public required string Email { get; set; }
        [DataType(DataType.PhoneNumber)] public required string MobileNumber { get; set; }
        [DataType(DataType.Password)] public required string Password { get; set; }

        [DataType(DataType.Text)] public string Rights { get; set; } = string.Empty; //AccessToUsers.User.ToString();
        [DataType(DataType.MultilineText)] public string? Address { get; set; }

        // Navigation property
        //public virtual ICollection<VehicleDetails> VehicleDetails { get; set; }

        [DataType(DataType.DateTime)] public DateTime? CreatedDate { get; set; } = DateTiming.GetIndianTime();
    }
}