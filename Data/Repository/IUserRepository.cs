using worksheet2.Model;

namespace worksheet2.Data.Repository
{
    public interface IUserRepository
    {
        User CreateUser(User user);

        User GetUserByUserId(string userId);
        User GetUserByUsername(string username);
        User GetUserByAccountNumber(string accountNumber);
        void Update(User user);
    }
}