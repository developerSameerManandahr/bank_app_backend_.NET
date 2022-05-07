using System.Collections.Generic;
using worksheet2.Model;

namespace worksheet2.Data.Repository
{
    public interface IAccountDetailRepository
    {
        void CreateAccountDetail(AccountDetails accountDetails);
        IEnumerable<AccountDetails> GetAccountDetailsByUser(User user);

        AccountDetails GetCurrentOrPremiumAccount(User user);

        AccountDetails GetAccountDetails(User user, AccountType accountType);

        void Update(AccountDetails accountDetails);
    }
}