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
        private static readonly List<User> users = [];
        private readonly IConfiguration configuration;

        public AuthService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public User Register(RegisterDto model)
        {
            var doesEmailExist = users.Any((u) => u.Email == model.Email);

            if (doesEmailExist)
            {
                throw new Exception("This email already exists");
            }

            // Hash the password before storing in database

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                Password = model.Password,
                Role = model.Role
            };

            users.Add(user);

            return user;
        }

        public UserTokenDto Login(LoginDto model)
        {
            // verify login credentials
            var user = users.FirstOrDefault((u) => u.Email == model.Email && u.Password == u.Password);
                
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
                new Claim(JwtRegisteredClaimNames.Iat, now.ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64),
                new Claim("Id", id.ToString()),
                new Claim("Email", email),
                new Claim("Role", role.ToString())
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
