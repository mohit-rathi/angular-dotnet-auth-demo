using Auth.Service.Dtos;
using Auth.Service.Interfaces;
using Auth.Service.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration configuration;
        private readonly IUserService userService;

        public AuthService(IConfiguration configuration, IUserService userService)
        {
            this.configuration = configuration.GetSection("JWTConfig");
            this.userService = userService;
        }

        public UserDto Register(RegisterDto model)
        {
            var doesEmailExist = userService.CheckEmailExists(model.Email);

            if (doesEmailExist)
            {
                throw new Exception("This email already exists");
            }

            // Hash the password before storing in database

            return userService.AddUser(model);
        }

        public UserTokenDto Login(LoginDto model)
        {
            // verify login credentials
            var user = userService.GetUserByCredentials(model.Email, model.Password);
                
            if (user == null)
            {
                throw new Exception("Incorrect email or password");
            }

            // generate access token
            var token = CreateToken(user.Id, user.Email, user.Role);

            return new UserTokenDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role,
                Token = token
            };
        }

        public string CreateToken(Guid id, string email, Role role)
        {
            // current timestamp
            var now = DateTime.UtcNow;

            // jwt claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Iat, ((DateTimeOffset)now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim("Id", id.ToString()),
                new Claim("Email", email),
                new Claim("Role", Enum.GetName(typeof(Role), role))
            };

            // signing key
            var symmetricKeyAsBase64 = configuration["SecretKey"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            // signing credentials
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // token options
            var tokenOptions = new JwtSecurityToken(
                issuer: configuration["Issuer"],
                audience: configuration["Audience"],
                claims: claims,
                expires: now.Add(TimeSpan.FromHours(24)),
                signingCredentials: signingCredentials
            );

            // create token
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }
    }
}
