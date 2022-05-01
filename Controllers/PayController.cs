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
    public class PayController : ControllerBase
    {
        private IPayService _payService;

        public PayController(IPayService payService)
        {
            _payService = payService;
        }

        [HttpPost("someone")]
        [Authorize]
        public IActionResult PaySomeone(PayRequest payRequest)
        {
            var user = (User) HttpContext.Items["User"];
            var response = _payService.pay(payRequest, user);

            return Ok(response);
        }

        [HttpPost("manage/fund")]
        [Authorize]
        public IActionResult ManageFund(ManageFundRequest manageFundRequest)
        {
            var user = (User) HttpContext.Items["User"];
            var response = _payService.manageFund(manageFundRequest, user);

            return Ok(response);
        }
    }
}