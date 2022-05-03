using System.ComponentModel.DataAnnotations;

namespace worksheet2.Model.Request
{
    public class ManageFundRequest
    {
        public AccountType FromAccountType { get; set; }
        public AccountType ToAccountType { get; set; }

        [Range(5, double.MaxValue, ErrorMessage = "Please enter amount that is greater than 5")]
        public long Amount { get; set; }
    }
}