using Microsoft.AspNetCore.Mvc;
using worksheet2.Model;
using worksheet2.Model.Request;
using worksheet2.Services;


namespace worksheet2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login/username")]
        public IActionResult AuthenticateByPassword(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new {message = "Username or password is incorrect"});

            return Ok(response);
        }

        [HttpPost("login/accountNumber")]
        public IActionResult AuthenticateByPin(AuthenticatePinRequest request)
        {
            var response = _userService.AuthenticateByPin(request);

            if (response == null)
                return BadRequest(new {message = "Pin or Account Number is incorrect"});

            return Ok(response);
        }

        [HttpPost("signup")]
        public IActionResult SignUp(SignupRequest model)
        {
            var response = _userService.SignUp(model);

            if (response == null)
                return BadRequest(new {message = "Username or password is incorrect"});

            return Ok(response);
        }
        
        [HttpPost("verify/pin")]
        public IActionResult VerifyPin(VerifyPinRequest model)
        {
            var user = (User) HttpContext.Items["User"];
            
            var response = _userService.VerifyPin(model, user);

            return Ok(response);
        }


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Items["User"] = null;
            return Ok("Logged out Successfully!");
        }
    }
}