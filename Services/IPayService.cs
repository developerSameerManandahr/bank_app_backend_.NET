using System.Collections.Generic;
using worksheet2.Model;
using worksheet2.Model.Request;
using worksheet2.Model.Response;

namespace worksheet2.Services
{
    public interface IPayService
    {
        BaseResponse pay(PayRequest payRequest, User fromUser);
        
        BaseResponse manageFund(ManageFundRequest manageFundRequest, User user);
    }
}