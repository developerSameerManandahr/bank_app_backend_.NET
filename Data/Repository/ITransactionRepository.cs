using System.Collections.Generic;
using worksheet2.Model;
using worksheet2.Model.Request;

namespace worksheet2.Data.Repository
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAllTransactionsForUser(string userId);

        void Add(Transaction transaction);
    }
}