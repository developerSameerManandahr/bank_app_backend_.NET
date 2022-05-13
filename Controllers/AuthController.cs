using System.Linq;
using Microsoft.AspNetCore.Mvc;
using worksheet2.Authentication;
using worksheet2.Model;
using worksheet2.Model.Request;
using worksheet2.Model.Response;
using worksheet2.Services;

namespace worksheet2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenService _tokenService;

        public AuthController(
            IAuthenticationService authenticationService,
            ITokenService tokenService)
        {
            _authenticationService = authenticationService;
            _tokenService = tokenService;
        }

        /**
         * Used to login by username
         */
        [HttpPost("login/username")]
        public IActionResult AuthenticateByPassword(AuthenticateRequest model)
        {
            var response = _authenticationService.Authenticate(model);

            if (response == null)
            {
                var error = new {message = "Username or password is incorrect"};
                return BadRequest(error);
            }

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
            {
                var error = new {message = "Pin or Account Number is incorrect"};
                return BadRequest(error);
            }

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
            {
                var error = new {message = "Username or password is incorrect"};
                return BadRequest(error);
            }


            return Ok(response);
        }


        /**
         * Used to verify the pin
         */
        [HttpPost("verify/pin")]
        [AuthorizationFilter]
        public IActionResult VerifyPin(VerifyPinRequest model)
        {
            var user = (User) HttpContext.Items["User"];

            var response = _authenticationService.VerifyPin(model.Pin, user);

            if (Helper.Helper.IsBadRequest(response))
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        /**
         * Used to logout
         */
        [HttpPost("logout")]
        [AuthorizationFilter]
        public IActionResult Logout()
        {
            HttpContext.Items["User"] = null;
            return Ok("Logged out Successfully!");
        }

        /**
         * Endpoint to verify the provided user details
         */
        [HttpPost("verify/details")]
        public IActionResult VerifyAccountDetails(VerifyAccountDetailsRequest request)
        {
            var response = _authenticationService.VerifyAccountDetails(request);

            if (Helper.Helper.IsBadRequest(response))
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /**
         * Endpoint to verify token
         */
        [HttpGet("verify/token")]
        public IActionResult VerifyToken()
        {
            var httpContextRequest = HttpContext.Request;
            var requestHeader = httpContextRequest.Headers["Authorization"];
            var firstOrDefault = requestHeader.FirstOrDefault();
            var token = firstOrDefault?.Split(" ").Last();
            if (!_tokenService.ValidateToken(token))
            {
                return BadRequest(new BaseResponse("Invalid token", "Error"));
            }

            return Ok(new BaseResponse("Token is valid", "Success"));
        }


        /**
         * Endpoint to change pin
         */
        [HttpPut("change/Pin")]
        [AuthorizationFilter]
        public IActionResult ChangePin(ChangePinRequest changePinRequest)
        {
            var user = (User) HttpContext.Items["User"];
            var baseResponse = _authenticationService.ChangePin(changePinRequest, user);
            if (Helper.Helper.IsBadRequest(baseResponse))
            {
                return BadRequest(baseResponse);
            }

            return Ok(baseResponse);
        }

        /**
         * Endpoint to change password
         */
        [HttpPut("change/Password")]
        [AuthorizationFilter]
        public IActionResult ChangePassword(ChangePasswordRequest changePassword)
        {
            var user = (User) HttpContext.Items["User"];
            var baseResponse = _authenticationService.ChangePassword(changePassword, user);
            if (Helper.Helper.IsBadRequest(baseResponse))
            {
                return BadRequest(baseResponse);
            }

            return Ok(baseResponse);
        }
    }
}