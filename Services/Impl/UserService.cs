using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using worksheet2.Data;
using worksheet2.Model;

namespace worksheet2.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly BankContext _context;

        public UserService(
            BankContext context
        )
        {
            _context = context;
        }

        public AccountDetails CreateAccountDetails(User createdUser, AccountType accountType)
        {
            var accountDetails = new AccountDetails
            {
                Balance = 0,
                Currency = "GBP",
                User = createdUser,
                AccountType = accountType
            };
            _context.AccountDetails
                .Add(accountDetails);

            _context.SaveChanges();

            _context.Entry(accountDetails).State = EntityState.Detached;
            return _context.AccountDetails.FirstOrDefault(details => details.User == createdUser);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users
                .Include(u => u.UserDetails)
                .ToList();
        }

        public User GetById(string id)
        {
            return _context.Users
                .Include(user => user.UserDetails)
                .FirstOrDefault(user => user.UserId == id);
        }
    }
}