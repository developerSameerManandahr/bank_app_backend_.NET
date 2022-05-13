using System.Collections.Generic;
using worksheet2.Model;
using worksheet2.Model.Response;

namespace worksheet2.Services
{
    public interface IAccountDetailsService
    {
        /**
         * Used to Get account details by user id
         */
        IEnumerable<AccountDetailsResponse> GetAccountDetailsResponsesById(User user);
    }
}