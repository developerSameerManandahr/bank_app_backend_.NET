using System;
using worksheet2.Data.Repository;
using worksheet2.Model;
using worksheet2.Model.Request;
using worksheet2.Model.Response;

namespace worksheet2.Services.Impl
{
    public class PayService : IPayService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IAccountDetailRepository _accountDetailRepository;
        private readonly ITransactionRepository _transactionRepository;

        public PayService(
            IUserRepository userRepository,
            IUserService userService,
            IAccountDetailRepository accountDetailRepository,
            ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _userService = userService;
            _accountDetailRepository = accountDetailRepository;
            _transactionRepository = transactionRepository;
        }

        public BaseResponse Pay(PayRequest payRequest, User fromUser)
        {
            var user = _userRepository.GetUserByAccountNumber(payRequest.To.AccountNumber);


            var fromAccountDetails = _accountDetailRepository.GetCurrentOrPremiumAccount(user);

            if (user == null) throw new NotImplementedException();

            var toAccountDetails = _accountDetailRepository.GetAccountDetails(user, AccountType.CURRENT);
            if (toAccountDetails == null)
                return new BaseResponse(
                    "Balance transferred ",
                    "Success"
                );
            TransferMoney(payRequest, toAccountDetails, fromAccountDetails);

            AddTransaction(payRequest, fromUser, user);


            return new BaseResponse(
                "Balance transferred ",
                "Success"
            );
        }

        public BaseResponse ManageFund(ManageFundRequest manageFundRequest, User user)
        {
            // Get Account Details 
            var fromAccountDetails =
                _accountDetailRepository.GetAccountDetails(user, manageFundRequest.FromAccountType);
            var toAccountDetails =
                _accountDetailRepository.GetAccountDetails(user, manageFundRequest.ToAccountType);

            //Deduct balance
            if (CheckBalanceOfSender(manageFundRequest, fromAccountDetails))
                fromAccountDetails.Balance -= manageFundRequest.Amount;
            else
                return new BaseResponse("insufficient balance", "Error");

            // Get Account Details of receiver and add balance
            var accountDetails = GetAccountDetails(manageFundRequest, user);
            if (toAccountDetails == null)
            {
                _accountDetailRepository.CreateAccountDetail(accountDetails);
                toAccountDetails = _accountDetailRepository.GetAccountDetails(user, manageFundRequest.ToAccountType);
            }

            toAccountDetails.Balance += manageFundRequest.Amount;

            // Update details of both sender and receiver
            _accountDetailRepository.Update(toAccountDetails);
            _accountDetailRepository.Update(fromAccountDetails);
            return new BaseResponse("Fund transferred", "Success");
        }

        private static bool CheckBalanceOfSender(
            ManageFundRequest manageFundRequest,
            AccountDetails fromAccountDetails)
        {
            return fromAccountDetails != null && fromAccountDetails.Balance > manageFundRequest.Amount;
        }

        private static AccountDetails GetAccountDetails(
            ManageFundRequest manageFundRequest,
            User user)
        {
            var accountDetails = new AccountDetails
            {
                Balance = 0,
                Currency = "GBP",
                User = user,
                AccountType = manageFundRequest.ToAccountType
            };
            return accountDetails;
        }

        private void TransferMoney(
            PayRequest payRequest,
            AccountDetails toAccountDetails,
            AccountDetails fromAccountDetails)
        {
            toAccountDetails.Balance += payRequest.Amount;
            _accountDetailRepository.Update(toAccountDetails);
            if (fromAccountDetails == null) return;
            fromAccountDetails.Balance -= payRequest.Amount;
            _accountDetailRepository.Update(fromAccountDetails);
        }

        private void AddTransaction(
            PayRequest payRequest,
            User fromUser, User user)
        {
            _transactionRepository
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