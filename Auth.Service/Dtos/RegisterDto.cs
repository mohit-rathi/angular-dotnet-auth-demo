using Auth.Service.Models;

namespace Auth.Service.Dtos
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
