using Auth.Service.Dtos;
using Auth.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public ActionResult<UserDto> Register(RegisterDto model)
        {
            return Ok(authService.Register(model));
        }

        [HttpPost("login")]
        public ActionResult<UserTokenDto> Login(LoginDto model)
        {
            return Ok(authService.Login(model));
        }
    }
}
