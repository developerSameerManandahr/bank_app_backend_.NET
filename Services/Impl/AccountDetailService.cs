using System.Collections.Generic;
using System.Linq;
using worksheet2.Data;
using worksheet2.Data.Repository;
using worksheet2.Model;
using worksheet2.Model.Response;

namespace worksheet2.Services.Impl
{
    public class AccountDetailService : IAccountDetailsService
    {
        private readonly IAccountDetailRepository _repository;

        public AccountDetailService(
            IAccountDetailRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<AccountDetailsResponse> GetAccountDetailsResponsesById(User user)
        {
            return _repository.GetAccountDetailsByUser(user)
                .Select(details => new AccountDetailsResponse
                {
                    Balance = details.Balance,
                    Currency = details.Currency,
                    AccountType = details.AccountType.ToString()
                });
        }
    }
}