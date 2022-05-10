using worksheet2.Model;
using worksheet2.Model.Request;
using worksheet2.Model.Response;

namespace worksheet2.Services
{
    public interface IAuthenticationService
    {
        AuthenticationResponse SignUp(SignupRequest request);
        BaseResponse VerifyAccountDetails(VerifyAccountDetailsRequest request);

        AuthenticationResponse Authenticate(AuthenticateRequest request);

        AuthenticationResponse AuthenticateByPin(AuthenticatePinRequest request);
        BaseResponse VerifyPin(string request, User user);
    }
}