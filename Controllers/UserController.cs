﻿using Microsoft.AspNetCore.Mvc;
using worksheet2.Authentication;
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

        [HttpGet("details")]
        [Authorize]
        public IActionResult Authenticate()
        {
            var response = _userService.GetAll();

            return Ok(response);
        }
    }
}