using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestProject.Service.IServices.Auth;

namespace TestProject.Controllers
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

        [HttpPost("login")]
        public async ValueTask<IActionResult> Login(string email)
        {
            var token = await authService.GenerateToken(email);
            return Ok(new
            {
                token
            });
        }
    }
}
