using System;
using System.Collections.Generic;
using System.Linq;
using worksheet2.Model;

namespace worksheet2.Data.Repository.Impl
{
    public sealed class TransactionRepository : ITransactionRepository, IDisposable
    {
        private readonly BankContext _context;

        private bool _disposed;

        public TransactionRepository(BankContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public List<Transaction> GetAllTransactionsForUser(string userId)
        {
            return _context.Transactions
                .Where(transaction => transaction.FromUserId == userId || transaction.ToUserId == userId)
                .ToList();
        }

        public void Add(Transaction record)
        {
            _context.Transactions
                .Add(record);
            _context.SaveChanges();
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();

            _disposed = true;
        }
    }
}