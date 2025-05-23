﻿using Auth.Service.Dtos;
using Auth.Service.Models;

namespace Auth.Service.Interfaces
{
    public interface IAuthService
    {
        UserDto Register(RegisterDto model);
        UserTokenDto Login(LoginDto model);
        string CreateToken(Guid id, string email, Role role);
    }
}
