using System.Collections.Generic;
using System.Linq;
using worksheet2.Data;
using worksheet2.Model;
using worksheet2.Model.Response;

namespace worksheet2.Services.Impl
{
    public class AccountDetailService : IAccountDetailsService
    {
        private readonly BankContext _context;

        public AccountDetailService(
            BankContext context)
        {
            _context = context;
        }

        public IEnumerable<AccountDetailsResponse> GetAccountDetailsResponsesById(User user)
        {
            return _context.AccountDetails
                .ToList()
                .Where(details => details.User == user)
                .Select(details => new AccountDetailsResponse
                {
                    Balance = details.Balance,
                    Currency = details.Currency,
                    AccountType = details.AccountType.ToString()
                });
        }
    }
}