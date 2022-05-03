using worksheet2.Model;
using worksheet2.Model.Request;
using worksheet2.Model.Response;

namespace worksheet2.Services
{
    public interface IPayService
    {
        BaseResponse Pay(PayRequest payRequest, User fromUser);

        BaseResponse ManageFund(ManageFundRequest manageFundRequest, User user);
    }
}