using Microsoft.AspNetCore.Mvc;
using worksheet2.Authentication;
using worksheet2.Model;
using worksheet2.Model.Request;
using worksheet2.Services;

namespace worksheet2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PayController : ControllerBase
    {
        private readonly IPayService _payService;

        public PayController(IPayService payService)
        {
            _payService = payService;
        }

        [HttpPost("someone")]
        [Authorize]
        public IActionResult PaySomeone(PayRequest payRequest)
        {
            var user = (User) HttpContext.Items["User"];
            var response = _payService.Pay(payRequest, user);

            if (Helper.Helper.IsBadRequest(response))
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("manage/fund")]
        [Authorize]
        public IActionResult ManageFund(ManageFundRequest manageFundRequest)
        {
            var user = (User) HttpContext.Items["User"];
            var response = _payService.ManageFund(manageFundRequest, user);
            if (Helper.Helper.IsBadRequest(response))
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}