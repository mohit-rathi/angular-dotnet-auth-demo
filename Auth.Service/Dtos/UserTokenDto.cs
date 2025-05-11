using Auth.Service.Models;

namespace Auth.Service.Dtos
{
    public class UserTokenDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }
    }
}
