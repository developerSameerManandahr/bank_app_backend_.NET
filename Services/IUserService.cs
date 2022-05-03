using System.Collections.Generic;
using worksheet2.Model;

namespace worksheet2.Services
{
    public interface IUserService
    {
        AccountDetails CreateAccountDetails(User createdUser, AccountType accountType);

        IEnumerable<User> GetAll();

        User GetById(string id);
    }
}