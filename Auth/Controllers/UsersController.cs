using Auth.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthService authService;

        public UsersController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpGet("Me")]
        public void Me()
        {

        }

        [HttpGet]
        public void S()
        {
            this.authService.
        }
    }
}
