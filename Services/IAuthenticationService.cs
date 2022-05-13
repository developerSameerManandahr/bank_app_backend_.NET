using worksheet2.Model;
using worksheet2.Model.Request;
using worksheet2.Model.Response;

namespace worksheet2.Services
{
    public interface IAuthenticationService
    {
        /**
         * Used to create a new user
         */
        AuthenticationResponse SignUp(SignupRequest request);
        
        /**
         * Used to verify account details
         */
        BaseResponse VerifyAccountDetails(VerifyAccountDetailsRequest request);

        /**
         * Authenticates the request by username and password
         */
        AuthenticationResponse Authenticate(AuthenticateRequest request);

        /**
         * Authenticates the request by account number and pin
         */
        AuthenticationResponse AuthenticateByPin(AuthenticatePinRequest request);
        
        /**
         * Verifies the pin provided with logged in user
         */
        BaseResponse VerifyPin(string request, User user);

        /**
         * Changes the pin of the logged in user
         */
        BaseResponse ChangePin(ChangePinRequest request, User user);

        /**
         * Changes the password of the logged in user
         */
        BaseResponse ChangePassword(ChangePasswordRequest request, User user);
    }
}