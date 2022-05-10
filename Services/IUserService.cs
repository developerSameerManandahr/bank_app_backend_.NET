using worksheet2.Model;

namespace worksheet2.Services
{
    public interface IUserService
    {
        User GetById(string id);
    }
}