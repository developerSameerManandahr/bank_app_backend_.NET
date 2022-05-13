using System;
using worksheet2.Data.Repository;
using worksheet2.Model;
using worksheet2.Model.Request;
using worksheet2.Model.Response;

namespace worksheet2.Services.Impl
{
    public class PayService : IPayService
    {
        private readonly IAccountDetailRepository _accountDetailRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        public PayService(
            IUserRepository userRepository,
            IAccountDetailRepository accountDetailRepository,
            ITransactionRepository transactionRepository,
            IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _accountDetailRepository = accountDetailRepository;
            _transactionRepository = transactionRepository;
            _authenticationService = authenticationService;
        }

        public BaseResponse Pay(PayRequest payRequest, User fromUser)
        {
            //Verify PIN
            if (!_authenticationService.VerifyPin(payRequest.Pin, fromUser).MessageType.Equals("Success"))
            {
                return new BaseResponse("PIN wrong", "Error");
            }

            // Verify the recipients details 
            var user = _userRepository.GetUserByAccountNumber(payRequest.To.AccountNumber);

            if (!CheckIfTheRequestIsGenuine(payRequest, user))
            {
                return new BaseResponse("Provided information is wrong !!", "Error");
            }
            
            //Check if the sender has enough balance
            var toAccountDetails = _accountDetailRepository.GetAccountDetails(user, AccountType.CURRENT);
            if (toAccountDetails == null || user == null)
                return new BaseResponse(
                    "Balance transfer failed ! Details is wrong ",
                    "Error"
                );
            
            var fromAccountDetails = _accountDetailRepository.GetCurrentOrPremiumAccount(fromUser);

            if (fromAccountDetails == null || fromAccountDetails.Balance <= payRequest.Amount)
            {
                return new BaseResponse(
                    "Insufficient Balance",
                    "Error"
                );
            }

            TransferMoney(payRequest, toAccountDetails, fromAccountDetails);

            AddTransaction(payRequest, fromUser, user);


            return new BaseResponse(
                "Balance transferred ",
                "Success"
            );
        }

        public BaseResponse ManageFund(ManageFundRequest manageFundRequest, User user)
        {
            if (!_authenticationService.VerifyPin(manageFundRequest.Pin, user).MessageType.Equals("Success"))
                return new BaseResponse("PIN Incorrect", "ERROR");
            // Get Account Details 
            var fromAccountDetails =
                _accountDetailRepository.GetAccountDetails(user, manageFundRequest.FromAccountType);
            var toAccountDetails =
                _accountDetailRepository.GetAccountDetails(user, manageFundRequest.ToAccountType);

            //check balance and deduct balance
            if (CheckBalanceOfSender(manageFundRequest, fromAccountDetails))
                fromAccountDetails.Balance -= manageFundRequest.Amount;
            else
                return new BaseResponse("Insufficient balance", "Error");

            // Get Account Details of receiver and add balance
            var accountDetails = GetAccountDetailsOfReceiver(manageFundRequest, user);
            if (toAccountDetails == null)
            {
                _accountDetailRepository.CreateAccountDetail(accountDetails);
                toAccountDetails =
                    _accountDetailRepository.GetAccountDetails(user, manageFundRequest.ToAccountType);
            }

            toAccountDetails.Balance += manageFundRequest.Amount;

            // Update details of both sender and receiver
            _accountDetailRepository.Update(toAccountDetails);
            _accountDetailRepository.Update(fromAccountDetails);
            return new BaseResponse("Fund transferred", "Success");
        }

        /**
         * Verifies the provided full name with actual full name got from the account number
         */
        private static bool CheckIfTheRequestIsGenuine(PayRequest payRequest, User user)
        {
            return payRequest.To.FullName.Equals(Helper.Helper.GetFullName(user));
        }

        /**
         * Checks if the sender has enough balance to send money
         */
        private static bool CheckBalanceOfSender(
            ManageFundRequest manageFundRequest,
            AccountDetails fromAccountDetails)
        {
            return fromAccountDetails != null && fromAccountDetails.Balance > manageFundRequest.Amount;
        }

        /**
         * Creates account details object for receiver
         */
        private static AccountDetails GetAccountDetailsOfReceiver(
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

        /**
         * deduct and add funds and updates
         */
        private void TransferMoney(
            PayRequest payRequest,
            AccountDetails toAccountDetails,
            AccountDetails fromAccountDetails)
        {
            fromAccountDetails.Balance -= payRequest.Amount;
            toAccountDetails.Balance += payRequest.Amount;
            _accountDetailRepository.Update(toAccountDetails);

            _accountDetailRepository.Update(fromAccountDetails);
        }

        /**
         * Adds transaction entry to the database
         */
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
                    Description = payRequest.Description,
                    User = fromUser,
                    TransactionDate = DateTime.Now
                });
        }
    }
}