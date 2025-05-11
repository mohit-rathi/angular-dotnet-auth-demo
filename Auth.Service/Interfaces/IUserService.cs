using Auth.Service.Dtos;

namespace Auth.Service.Interfaces
{
    public interface IUserService
    {
        List<UserDto> GetUsers();
        bool CheckEmailExists(string email);
        UserDto GetUserByCredentials(string email, string password);
        UserDto AddUser(RegisterDto model);
    }
}
