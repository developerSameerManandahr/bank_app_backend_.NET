using System;

namespace worksheet2.Model.Response
{
    public class TransactionResponse
    {
        public string TransactionType { get; set; }
        public long Amount { get; set; }
        
        public string BeneficiaryUser { get; set; }
        
        public string Description { get; set; }
        
        public DateTime DateOfTransaction { get; set; }
    }
}