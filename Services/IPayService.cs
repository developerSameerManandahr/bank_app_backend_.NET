using worksheet2.Model;
using worksheet2.Model.Request;
using worksheet2.Model.Response;

namespace worksheet2.Services
{
    public interface IPayService
    {
        /**
         * Used to transfer balance to the account of someone else
         */
        BaseResponse Pay(PayRequest payRequest, User fromUser);

        /**
         * Used to transfer balance within own account 
         */
        BaseResponse ManageFund(ManageFundRequest manageFundRequest, User user);
    }
}