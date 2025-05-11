using Auth.Service.Dtos;
using Auth.Service.Interfaces;
using Auth.Service.Models;

namespace Auth.Service.Services
{
    public class UserService : IUserService
    {
        private static readonly List<User> users = [];

        public UserDto AddUser(RegisterDto model)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                Password = model.Password,
                Role = model.Role
            };

            users.Add(user);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            };
        }

        public bool CheckEmailExists(string email)
        {
            return users.Any((u) => u.Email == email);
        }

        public UserDto GetUserByCredentials(string email, string password)
        {
            var user = users.FirstOrDefault((u) => u.Email == email && u.Password == password);

            return user != null ? new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            } : null;
        }

        public UserDto GetUserById(Guid id)
        {
            var user = users.FirstOrDefault((u) => u.Id == id);

            return user != null ? new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            } : null;
        }

        public List<UserDto> GetUsers()
        {
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Email = u.Email,
                Role = u.Role
            }).ToList();
        }
    }
}
