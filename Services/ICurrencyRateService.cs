using System.Threading.Tasks;
using worksheet2.Model.Response;

namespace worksheet2.Services
{
    public interface ICurrencyRateService
    {
        /**
         * Gets exchange rates for USD and EUR with base GBP
         */
        Task<Exchange> GetExchangeRates();
    }
}