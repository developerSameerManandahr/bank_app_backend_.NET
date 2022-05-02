using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using worksheet2.Data;
using worksheet2.Model;
using worksheet2.Model.Response;

namespace worksheet2.Services.Impl
{
    public class TransactionService : ITransactionService
    {
        private readonly BankContext _context;

        public TransactionService(BankContext context)
        {
            _context = context;
        }

        public List<TransactionResponse> GetTransactionResponses(User user)
        {
            var transactions = _context.Transactions
                .Where(transaction => transaction.FromUserId == user.UserId || transaction.ToUserId == user.UserId)
                .ToList();

            return (from transaction in transactions
                let beneficiaryUserId = transaction.FromUserId == user.UserId
                    ? transaction.ToUserId
                    : transaction.FromUserId
                let beneficiaryUsername = _context.Users.FirstOrDefault(user1 => user1.UserId == beneficiaryUserId)
                    ?.UserName
                select new TransactionResponse
                {
                    Amount = transaction.Amount,
                    Description = transaction.Description,
                    BeneficiaryUser = beneficiaryUsername,
                    TransactionType = transaction.FromUserId == user.UserId ? "DR" : "CR",
                    DateOfTransaction = transaction.TransactionDate
                }).ToList();
        }

        private static Expression<Func<User, bool>> GetUser(User user, Transaction transaction)
        {
            return user1 => user.UserId ==
                            (transaction.FromUserId == user.UserId
                                ? transaction.ToUserId
                                : transaction.FromUserId);
        }
    }
}