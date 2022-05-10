using System.Collections.Generic;
using worksheet2.Model;

namespace worksheet2.Data.Repository
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAllTransactionsForUser(string userId);

        void Add(Transaction transaction);
    }
}