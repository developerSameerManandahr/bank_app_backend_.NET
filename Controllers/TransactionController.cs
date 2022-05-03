using Microsoft.AspNetCore.Mvc;
using worksheet2.Authentication;
using worksheet2.Model;
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
        public IActionResult Authenticate()
        {
            var user = (User) HttpContext.Items["User"];
            var response = _transactionService.GetTransactionResponses(user);
            return Ok(response);
        }
    }
}