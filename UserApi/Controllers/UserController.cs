using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using UserApi.DTOs;
using UserApi.Interfaces;
using UserApi.Services;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto request)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (adminId == null) return Unauthorized("Not authorized");

            var adminGuid = Guid.Parse(adminId!);

            var id = await _userService.Create(request, adminGuid);

            if(id == null) return BadRequest();

            return Ok(id);
        }

        [Authorize]
        [HttpPut()]
        public IActionResult Update([FromBody] UpdateUserDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized("Not authorized");

            throw new NotImplementedException();
        }

        [Authorize]
        [HttpPatch("password")]
        public IActionResult ChangePassword()
        {
            throw new NotImplementedException();
        }

        [HttpPatch("login")]
        public IActionResult ChangeLogin()
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("login")]
        public IActionResult GetByLogin()
        {
            throw new NotImplementedException();
        }

        [HttpGet("data")]
        public IActionResult GetByData()
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("age")]
        public IActionResult GetAllByAge()
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult Delete()
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        public IActionResult Restore()
        {
            throw new NotImplementedException();
        }
    }
}
