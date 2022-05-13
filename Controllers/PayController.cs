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

        /**
         * Endpoint to pay someone or transfer money to other account
         */
        [HttpPost("someone")]
        [AuthorizationFilter]
        public IActionResult PaySomeone(PayRequest payRequest)
        {
            var user = GetUser();
            var response = _payService.Pay(payRequest, user);

            if (Helper.Helper.IsBadRequest(response))
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /**
         * Endpoint to transfer balance from one account type to other
         */
        [HttpPost("manage/fund")]
        [AuthorizationFilter]
        public IActionResult ManageFund(ManageFundRequest manageFundRequest)
        {
            var user = GetUser();
            var response = _payService.ManageFund(manageFundRequest, user);
            if (Helper.Helper.IsBadRequest(response))
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        private User GetUser()
        {
            var httpContextItem = HttpContext.Items["User"];
            return (User) httpContextItem;
        }
    }
}