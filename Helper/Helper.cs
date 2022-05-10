using worksheet2.Model.Response;

namespace worksheet2.Helper
{
    public class Helper
    {
        public static bool IsBadRequest(BaseResponse response)
        {
            return !response.MessageType.ToLower().Equals("success");
        }
    }
}