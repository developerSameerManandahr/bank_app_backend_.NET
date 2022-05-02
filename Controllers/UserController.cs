using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using worksheet2.Authentication;
using worksheet2.Model;
using worksheet2.Model.Request;
using worksheet2.Services;


namespace worksheet2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("userDetails")]
        [Authorize]
        public IActionResult Authenticate()
        {
            var response = _userService.GetAll();

            return Ok(response);
        }
    }
}