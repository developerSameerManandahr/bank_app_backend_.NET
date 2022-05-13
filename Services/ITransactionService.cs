using System.Collections.Generic;
using worksheet2.Model;
using worksheet2.Model.Response;

namespace worksheet2.Services
{
    public interface ITransactionService
    {
        /**
         * Gets all transaction for the user
         */
        IEnumerable<TransactionResponse> GetTransactionResponses(User user);
    }
}