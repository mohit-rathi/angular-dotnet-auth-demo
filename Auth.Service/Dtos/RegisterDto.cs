namespace Auth.Service.Dtos
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
}
