using Microsoft.AspNetCore.Mvc;
using worksheet2.Authentication;
using worksheet2.Model;
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
            this._accountDetailsService = accountDetailsService;
        }

        [HttpGet("accountDetails")]
        [Authorize]
        public IActionResult GetAccountDetails()
        {
            var user = (User) HttpContext.Items["User"];
            if (user == null) return BadRequest(new {message = "Bad Token"});
            var response = _accountDetailsService
                .getAccountDetailsResponsesById(user);

            return Ok(response);
        }
    }
}