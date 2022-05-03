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
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /**
         * Used to login by username
         */
        [HttpPost("login/username")]
        public IActionResult AuthenticateByPassword(AuthenticateRequest model)
        {
            var response = _authenticationService.Authenticate(model);

            if (response == null)
                return BadRequest(new {message = "Username or password is incorrect"});

            return Ok(response);
        }

        /**
         * Used to login by accountNumber
         */
        [HttpPost("login/accountNumber")]
        public IActionResult AuthenticateByPin(AuthenticatePinRequest request)
        {
            var response = _authenticationService.AuthenticateByPin(request);

            if (response == null)
                return BadRequest(new {message = "Pin or Account Number is incorrect"});

            return Ok(response);
        }

        /**
         * Used to signup
         */
        [HttpPost("signup")]
        public IActionResult SignUp(SignupRequest model)
        {
            var response = _authenticationService.SignUp(model);

            if (response == null)
                return BadRequest(new {message = "Username or password is incorrect"});

            return Ok(response);
        }


        /**
         * Used to verify the pin
         */
        [HttpPost("verify/pin")]
        public IActionResult VerifyPin(VerifyPinRequest model)
        {
            var user = (User) HttpContext.Items["User"];

            var response = _authenticationService.VerifyPin(model, user);

            return Ok(response);
        }


        /**
         * Used to logout
         */
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Items["User"] = null;
            return Ok("Logged out Successfully!");
        }
    }
}