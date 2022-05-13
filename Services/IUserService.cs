using worksheet2.Model;
using worksheet2.Model.Request;
using worksheet2.Model.Response;

namespace worksheet2.Services
{
    public interface IUserService
    {
        /**
         * Returns uses for the provided id
         */
        User GetById(string id);

        /**
         * Update used details
         */
        BaseResponse Update(UpdateUserDetailsRequest request, User user);
    }
}