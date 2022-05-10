using worksheet2.Data.Repository;
using worksheet2.Model;

namespace worksheet2.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User GetById(string id)
        {
            return _repository.GetUserByUserId(id);
        }
    }
}