using CarParkingBookingDatabase.DBModel;

namespace CarParkingBookingDatabase.dummyData;

public static class SampleData
{
    public static object[] Data =
    {
        new UserDetails() { UserID="User-1", Email="balaji@gmail.com",Password="balaji",Name="balaji",MobileNumber="7896541235" },
        new DealerDetails()
        {
            DealerID = "Dealer-1" ,DealerName = "surya", DealerEmail = "surya@gmail.com", DealerPassword = "surya",
            DealerPhoneNo = "5912364782",CreatedDate = new DateTime(2020,05,22), DealerAddress = "Address data",
            DealerDescription = "Dealer description", DealerGPSLocation = "URL",DealerLandmark = "Landmark",DealerRating = "3.3",
            DealerTiming = "{\"monday\": {\"start\": \"6:54 am\", \"stop\": \"5:45 pm\"},\"tuesday\": { \"start\": \"6:54 am\", \"stop\": \"5:45 pm\"},\"wednesday\": { \"start\": \"6:54 am\", \"stop\": \"5:45 pm\"},\"thursday\": { \"start\": \"6:54 am\", \"stop\": \"5:45 pm\"},\"friday\": { \"start\": \"6:54 am\", \"stop\": \"5:45 pm\"},\"saturday\": { \"start\": \"6:54 am\", \"stop\": \"5:45 pm\"},\"sunday\": { \"start\": \"6:54 am\", \"stop\": \"5:45 pm\"},\"alwaysAvailable\": \"false\"}",
            DealerStoreName = "MuthuTheMass",DealerOpenOrClosed = false
        },
        new BookingDetails()
        {
            BookingID = "Booking-1",ArrivingTime = new DateTime().ToLocalTime(),CreatedDate = new DateTime().ToLocalTime().AddHours(-1),
            Dealer_ID = "Dealer-1", Driver_Name = "surya",Driver_PhoneNumber = "7536984126", User_ID = "User-1", Vehicle_Id = "Vehicle-1"
        },
        new VehicleDetails()
        {
            VehicleId = "Vehicle-1",Alternative_Phone_Number = "7896321456" , CreatedDate = new DateTime().ToLocalTime(),
            VehicleImage = byteArrayImage(),UserID = "User-1", VehicleName = "swift" , VehicleNumber = "TN 09 HR 9876"
        }
    };

    private static byte[] byteArrayImage()
    {
        byte[] byteArray = System.IO.File.ReadAllBytes(Path.GetFullPath(@".\MuthuTheMass\CarParkingBookingDatabase\dummyData\OIP.jpg"));
        return byteArray;
    }
    
}