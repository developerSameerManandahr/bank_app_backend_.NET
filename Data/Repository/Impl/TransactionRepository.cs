using System;
using System.Collections.Generic;
using System.Linq;
using worksheet2.Model;

namespace worksheet2.Data.Repository.Impl
{
    public sealed class TransactionRepository : ITransactionRepository, IDisposable
    {
        private readonly BankContext _context;

        public TransactionRepository(BankContext context)
        {
            _context = context;
        }

        public List<Transaction> GetAllTransactionsForUser(string userId)
        {
            return _context.Transactions
                .Where(transaction => transaction.FromUserId == userId || transaction.ToUserId == userId)
                .ToList();
        }

        public void Add(Transaction transaction)
        {
            _context.Transactions
                .Add(transaction);
        }

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}