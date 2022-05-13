using worksheet2.Model;
using worksheet2.Model.Response;

namespace worksheet2.Helper
{
    public static class Helper
    {
        /**
         * Checks if the BaseResponse is bad
         */
        public static bool IsBadRequest(BaseResponse response)
        {
            return !response.MessageType.ToLower().Equals("success");
        }
        
        public static string GetFullName(User user)
        {
            var middleName = user.UserDetails.MiddleName is {Length: > 0}
                ? user.UserDetails.MiddleName + " "
                : user.UserDetails.MiddleName;
            return user.UserDetails.FirstName + " " + middleName + user.UserDetails.LastName;
        }
    }
}