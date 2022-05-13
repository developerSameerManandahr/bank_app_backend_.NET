using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using worksheet2.Model.Response;
using worksheet2.Model.Settings;

namespace worksheet2.Services.Impl
{
    public class CurrencyRateService : ICurrencyRateService
    {
        private readonly AppSettings _appSettings;
        private readonly HttpClient _client;

        private readonly IMemoryCache _memoryCache;

        public CurrencyRateService(
            IOptions<AppSettings> appSettings,
            IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _appSettings = appSettings.Value;
            _client = new HttpClient();
        }

        public async Task<Exchange> GetExchangeRates()
        {
            //if it is in cache get it from cache to reduce the need of creating a HTTP request
            if (_memoryCache.TryGetValue("Exchange", out Exchange exchange)) return exchange;

            var query = new Dictionary<string, string>
            {
                ["base"] = _appSettings.BaseCurrency,
                ["symbols"] = "USD,EUR"
            };
            var uri = QueryHelpers.AddQueryString(_appSettings.ExchangeApiUrl, query);

            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
                exchange = await response
                    .Content
                    .ReadFromJsonAsync<Exchange>();

            StoreIntoCache(exchange);

            return exchange;
        }

        /**
         * Stores the currency rates into the cache for few hours.
         */
        private void StoreIntoCache(Exchange exchange)
        {
            // Set cache options.
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromHours(_appSettings.CacheLifeInHours));

            _memoryCache.Set("Exchange", exchange, cacheEntryOptions);
        }
    }
}