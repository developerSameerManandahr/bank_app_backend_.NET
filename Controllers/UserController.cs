using worksheet2.Authentication;
using Microsoft.AspNetCore.Mvc;
using worksheet2.Model;
using worksheet2.Model.Request;
using worksheet2.Services;

namespace worksheet2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        /**
         * Endpoint to update user details
         */
        [HttpPut("update/details")]
        [AuthorizationFilter]
        public IActionResult UpdateDetails(UpdateUserDetailsRequest request)
        {
            var user = (User) HttpContext.Items["User"];
            return Ok(_userService.Update(request, user));
        }
    }
}