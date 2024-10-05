namespace CarParkingBookingVM.Authorization
{
    public class LoginVM
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class AuthorizedLoginVM
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Access { get; set; }
        public string AccessToken { get; set; }
    }

    public class DealerLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthorizedDealerLoginVM
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Access { get; set; }
        public string AccessToken { get; set; }
    }

}
