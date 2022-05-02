using System.Collections.Generic;
using worksheet2.Model;
using worksheet2.Model.Request;
using worksheet2.Model.Response;

namespace worksheet2.Services
{
    public interface IAuthenticationService
    {
        BaseResponse SignUp(SignupRequest request);

        AuthenticationResponse Authenticate(AuthenticateRequest request);

        AuthenticationResponse AuthenticateByPin(AuthenticatePinRequest request);
        BaseResponse VerifyPin(VerifyPinRequest request, User user);
    }
}