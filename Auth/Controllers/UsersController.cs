using Auth.Service.Dtos;
using Auth.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("Me")]
        public ActionResult<UserDto> Me()
        {
            var id = Guid.Parse(User.FindFirstValue("Id"));
            return Ok(userService.GetUserById(id));
        }

        [HttpGet]
        public ActionResult<List<UserDto>> GetUsers()
        {
            return Ok(userService.GetUsers());
        }
    }
}
