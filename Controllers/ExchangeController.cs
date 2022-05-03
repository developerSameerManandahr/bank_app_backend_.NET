using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using worksheet2.Services;

namespace worksheet2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeController : ControllerBase
    {
        private readonly ICurrencyRateService _service;

        public ExchangeController(ICurrencyRateService service)
        {
            _service = service;
        }

        [HttpPost("convert")]
        public async Task<IActionResult> PaySomeone()
        {
            var exchangeRates = await _service.GetExchangeRates();
            return Ok(exchangeRates);
        }
    }
}