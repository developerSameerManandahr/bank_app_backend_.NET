using System;
using System.Collections.Generic;
using System.Linq;
using worksheet2.Model;

namespace worksheet2.Data.Repository.Impl
{
    /**
     * Implements IDisposable to dispose the context after its use
     */
    public sealed class AccountDetailRepository : IAccountDetailRepository, IDisposable
    {
        private readonly BankContext _context;

        private bool _disposed;

        public AccountDetailRepository(BankContext context)
        {
            _context = context;
        }


        public void CreateAccountDetail(AccountDetails accountDetails)
        {
            _context.AccountDetails
                .Add(accountDetails);
            _context.SaveChanges();
        }

        public IEnumerable<AccountDetails> GetAccountDetailsByUser(User user)
        {
            return _context.AccountDetails
                .ToList()
                .Where(details => details.User == user);
        }

        public AccountDetails GetCurrentOrPremiumAccount(User user)
        {
            return _context.AccountDetails
                .FirstOrDefault(details => details.User == user &&
                                           (details.AccountType == AccountType.CURRENT ||
                                            details.AccountType == AccountType.PREMIUM));
        }

        public AccountDetails GetAccountDetails(User user, AccountType accountType)
        {
            return _context.AccountDetails
                .FirstOrDefault(details => details.User == user &&
                                           details.AccountType == accountType);
        }

        public void Update(AccountDetails accountDetails)
        {
            _context.AccountDetails
                .Update(accountDetails);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();

            _disposed = true;
        }
    }
}