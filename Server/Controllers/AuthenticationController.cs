using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Requests;
using SharedLibrary.Responses;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public IActionResult Register(AuthenticationRequest requset)
        {
            var (success, content) = _authenticationService.Register(requset.Username, requset.Password);
            if(!success) return BadRequest(content);

            return Login(requset);
        }

        [HttpPost("login")]
        public IActionResult Login(AuthenticationRequest requset)
        {
            var (success, content) = _authenticationService.Login(requset.Username, requset.Password);
            if (!success) return BadRequest(content);

            return Ok(new AuthenticationResponse() { Token = content });
        }


    }
}
