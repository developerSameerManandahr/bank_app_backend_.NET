using System.ComponentModel.DataAnnotations;

namespace worksheet2.Model.Request
{
    /**
     * Request model that is required while moving fund from one account type to other from API
     */
    public class ManageFundRequest
    {
        public AccountType FromAccountType { get; set; }
        public AccountType ToAccountType { get; set; }

        [Range(5, double.MaxValue, ErrorMessage = "Please enter amount that is greater than 5")]
        public long Amount { get; set; }
        
        public string Pin { get; set; }
    }
}