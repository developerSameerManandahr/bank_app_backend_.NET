using System;
using System.Linq;
using worksheet2.Data;
using worksheet2.Model;
using worksheet2.Model.Request;
using worksheet2.Model.Response;

namespace worksheet2.Services.Impl
{
    public class PayService : IPayService
    {
        private readonly BankContext _context;
        private readonly IUserService _userService;

        public PayService(BankContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public PayService(BankContext context)
        {
            _context = context;
        }

        public BaseResponse pay(PayRequest payRequest, User fromUser)
        {
            var user = _context.Users
                .FirstOrDefault(user => user.UserName == payRequest.To);
            var fromAccountDetails = _context.AccountDetails
                .FirstOrDefault(details => details.User == fromUser && (details.AccountType == AccountType.CURRENT ||
                                                                          details.AccountType == AccountType.PREMIUM));

            if (user == null) throw new System.NotImplementedException();


            var toAccountDetails = _context.AccountDetails
                .FirstOrDefault(details => details.User == user && details.AccountType == AccountType.CURRENT);
            if (toAccountDetails != null)
            {
                TransferMoney(payRequest, toAccountDetails, fromAccountDetails);

                AddTransaction(payRequest, fromUser, user);

                _context.SaveChanges();
            }


            return new BaseResponse(
                "Balance transferred ",
                "Success"
            );
        }

        public BaseResponse manageFund(ManageFundRequest manageFundRequest, User user)
        {
            var fromAccountDetails = GetAccountDetails(manageFundRequest.fromAccountType, user);
            var toAccountDetails = GetAccountDetails(manageFundRequest.toAccountType, user);

            if (fromAccountDetails != null && fromAccountDetails.Balance > manageFundRequest.amount)
            {
                fromAccountDetails.Balance -= manageFundRequest.amount;
            }
            else
            {
                return new BaseResponse("insufficient balance", "Error");
            }

            toAccountDetails ??= _userService.CreateAccountDetails(user, manageFundRequest.toAccountType);

            toAccountDetails.Balance += manageFundRequest.amount;

            _context.SaveChanges();
            return new BaseResponse("Fund transferred", "Success");
        }

        private AccountDetails? GetAccountDetails(AccountType accountType, User user)
        {
            return _context.AccountDetails
                .FirstOrDefault(details =>
                    details.User == user && details.AccountType == accountType);
        }

        private void TransferMoney(PayRequest payRequest, AccountDetails toAccountDetails,
            AccountDetails fromAccountDetails)
        {
            toAccountDetails.Balance += payRequest.Amount;
            if (fromAccountDetails != null)
            {
                fromAccountDetails.Balance -= payRequest.Amount;
                _context.AccountDetails.Update(fromAccountDetails);
            }
        }

        private void AddTransaction(PayRequest payRequest, User fromUser, User? user)
        {
            _context.Transactions
                .Add(new Transaction
                {
                    FromUserId = fromUser.UserId,
                    ToUserId = user.UserId,
                    Amount = payRequest.Amount,
                    Description = "transferred",
                    User = fromUser,
                    TransactionDate = DateTime.Now
                });
        }
    }
}