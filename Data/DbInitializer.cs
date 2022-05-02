using System.Linq;
using System.Web.Helpers;
using worksheet2.Model;

namespace worksheet2.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BankContext context)
        {
            context.Database.EnsureCreated();

            // Look for any users.
            if (context.Users.Any())
            {
                return; // DB has been seeded
            }

            var user = new User
            {
                UserId = "1",
                UserName = "root",
                Password = Crypto.HashPassword("root"), //var verifyPassword = Crypto.VerifyHashedPassword(hash, "root");
                Pin = Crypto.HashPassword("1234"),
                AccountNumber = "0123456789"
            };

            var accountDetails = new AccountDetails
            {
                Balance = 1000000000,
                Currency = "GBP",
                User = user,
                AccountType = AccountType.PREMIUM
            };

            var userDetails = new UserDetails
            {
                User = user,
                FirstName = "Ussop",
                LastName = "God",
                MiddleName = "sogeking",
                UserUserDetailsId = "1",
                Address = "Home",
                PhoneNumber = "1111111111"
            };

            context.Users.Add(user);
            context.AccountDetails.Add(accountDetails);
            context.UserDetails.Add(userDetails);

            context.SaveChanges();
        }
    }
}