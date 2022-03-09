using System.Linq;
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

            var users = new User[]
            {
                new User
                {
                    UserId = "1",
                    UserName = "root",
                    Password = "root",
                    Pin = 1234,
                    AccountNumber = "0123456789"
                }
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();
        }
    }
}