using System.Collections.Generic;
using System.Linq;
using worksheet2.Data.Repository;
using worksheet2.Model;
using worksheet2.Model.Response;

namespace worksheet2.Services.Impl
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;

        public TransactionService(
            IUserRepository userRepository,
            ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
        }

        public IEnumerable<TransactionResponse> GetTransactionResponses(User user)
        {
            var transactions = _transactionRepository.GetAllTransactionsForUser(user.UserId);

            return (from transaction in transactions
                let beneficiaryUserId = transaction.FromUserId == user.UserId
                    ? transaction.ToUserId
                    : transaction.FromUserId
                let currentUser = _userRepository.GetUserByUserId(beneficiaryUserId)
                let beneficiaryUsername = Helper.Helper.GetFullName(currentUser)
                select new TransactionResponse
                {
                    Amount = transaction.Amount,
                    Description = transaction.Description,
                    BeneficiaryUser = beneficiaryUsername,
                    TransactionType = transaction.FromUserId == user.UserId ? "DR" : "CR",
                    DateOfTransaction = transaction.TransactionDate
                }).ToList();
        }
    }
}