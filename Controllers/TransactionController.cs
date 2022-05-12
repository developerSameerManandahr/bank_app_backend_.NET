using Microsoft.AspNetCore.Mvc;
using worksheet2.Authentication;
using worksheet2.Model;
using worksheet2.Model.Response;
using worksheet2.Services;

namespace worksheet2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("view")]
        [Authorize]
        public IActionResult ViewTransaction()
        {
            var user = (User) HttpContext.Items["User"];
            if (user == null) return BadRequest(new {message = "Bad Token"});
            var response = _transactionService.GetTransactionResponses(user);
            return Ok(new BaseResponse("Fetched Transaction Details Successfully", "Success", response));
        }
    }
}