using System;
using System.ComponentModel.DataAnnotations;

namespace worksheet2.Model.Request
{
    public class ManageFundRequest
    {
        public AccountType fromAccountType { get; set; }
        public AccountType toAccountType { get; set; }
        
        [Range(5, Double.MaxValue, ErrorMessage = "Please enter amount that is greater than 5")]
        public long amount { get; set; }
    }
}