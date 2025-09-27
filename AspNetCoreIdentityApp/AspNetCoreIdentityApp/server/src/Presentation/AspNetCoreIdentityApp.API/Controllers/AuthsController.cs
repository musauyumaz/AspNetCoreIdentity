using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentityApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        public async Task<IActionResult> Login()
        {
            return Ok("Merhaba Dünya");
        }

        public async Task<IActionResult> SignUp()
        {
            return Ok("Merhaba Dünya");
        }
    }
}
