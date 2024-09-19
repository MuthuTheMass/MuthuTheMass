namespace CarParkingBookingVM.Authorization
{
    public class LoginVM
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class AuthorizedLoginVM
    {
        public string Email { get; set; }

        public string Access { get; set; }
    }
}
