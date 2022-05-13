using Microsoft.AspNetCore.Mvc;
using worksheet2.Authentication;
using worksheet2.Model;
using worksheet2.Model.Response;
using worksheet2.Services;

namespace worksheet2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountDetailsService _accountDetailsService;

        public AccountController(IAccountDetailsService accountDetailsService)
        {
            _accountDetailsService = accountDetailsService;
        }

        /**
         * Endpoint to get Account Details
         */
        [HttpGet("Details")]
        [AuthorizationFilter]
        public IActionResult GetAccountDetails()
        {
            var user = (User) HttpContext.Items["User"];
            if (user == null)
            {
                var error = new {message = "Bad Token"};
                return BadRequest(error);
            }

            var response = _accountDetailsService
                .GetAccountDetailsResponsesById(user);

            var baseResponse = new BaseResponse("Fetched Account Details Successfully", "Success", response);
            return Ok(baseResponse);
        }
    }
}