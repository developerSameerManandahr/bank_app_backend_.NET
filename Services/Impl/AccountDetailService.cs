using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
            this._context = context;
        }

        public IEnumerable<AccountDetailsResponse> getAccountDetailsResponsesById(User user)
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