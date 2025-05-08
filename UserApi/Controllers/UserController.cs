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

            var adminGuid = Guid.Parse(adminId);

            var id = await _userService.Create(request, adminGuid);

            if(id == null) return BadRequest();

            return Ok(id);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateData([FromBody] UpdateUserDataDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized("Not authorized");

            var userGuid = Guid.Parse(userId);
            
            var response = await _userService.UpdateData(request, userGuid);

            return Ok(response);
        }

        [Authorize]
        [HttpPatch("update-password")]
        public async Task<IActionResult> ChangePassword([FromBody] UserPasswordDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized("Not authorized");

            var userGuid = Guid.Parse(userId);

            var response = await _userService.ChangePassword(request, userGuid);

            return Ok(response);
        }

        [Authorize]
        [HttpPatch("update-login")]
        public async Task<IActionResult> ChangeLogin([FromBody] UserLoginDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized("Not authorized");

            var userGuid = Guid.Parse(userId);

            var response = await _userService.ChangeLogin(request, userGuid);

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized("Not authorized");

            var userGuid = Guid.Parse(userId);

            var response = await _userService.GetAll(userGuid);

            if (response == null) return Unauthorized("Not authorized");

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("get-by-login")]
        public async Task<IActionResult> GetByLogin([FromBody] LoginDto dto)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (adminId == null) return Unauthorized("Not authorized");

            var adminGuid = Guid.Parse(adminId);

            var response = await _userService.GetByLogin(dto, adminGuid);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [Authorize]
        [HttpPost("get-by-data")]
        public async Task<IActionResult> GetByData(SignInRequest dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized("Not authorized");

            var userGuid = Guid.Parse(userId);

            var response = await _userService.GetByData(dto, userGuid);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{age}")]
        public async Task<IActionResult> GetAllByOverAge(int age)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized("Not authorized");

            var userGuid = Guid.Parse(userId);

            var response = await _userService.GetAllByOverAge(userGuid, age);

            if (response == null) return Unauthorized("Not authorized");

            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] UserDeleteDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized("Not authorized");

            var userGuid = Guid.Parse(userId);

            var response = await _userService.Delete(dto, userGuid);

            if (response == null) return NotFound();
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("restore")]
        public async Task<IActionResult> Restore([FromBody] LoginDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized("Not authorized");

            var userGuid = Guid.Parse(userId);

            var response = await _userService.Restore(dto, userGuid);

            if (response == null) return NotFound();
            return Ok(response);
        }
    }
}
