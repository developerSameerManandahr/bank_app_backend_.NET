using worksheet2.Data.Repository;
using worksheet2.Model;
using worksheet2.Model.Request;
using worksheet2.Model.Response;

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

        public BaseResponse Update(UpdateUserDetailsRequest request, User user)
        {
            var userUserDetails = user.UserDetails;
            userUserDetails.Address = request.Address;
            userUserDetails.PhoneNumber = request.PhoneNumber;
            _repository.Update(user);
            return new BaseResponse("Updated successfully", "Success");
        }
    }
}