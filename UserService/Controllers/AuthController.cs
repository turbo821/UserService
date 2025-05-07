using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController
    {

        [HttpPost]
        public async Task<IActionResult> Login()
        {
            throw new NotImplementedException();
        }
    }
}
