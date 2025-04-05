namespace CarParkingSystem.Application.Dtos.Authorization
{
    public class LoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class AuthorizedLoginDto
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Access { get; set; }
        public string AccessToken { get; set; }
    }
}