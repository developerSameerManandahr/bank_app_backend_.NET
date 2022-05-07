using System.ComponentModel.DataAnnotations;

namespace worksheet2.Model.Request
{
    public class PayRequest
    {
        public ToCredentials To { get; set; }
        public long Amount { get; set; }
    }

    public abstract class ToCredentials
    {
        public string FullName { get; set; }

        [MinLength(10)]
        [MaxLength(10)]
        public string AccountNumber { get; set; }
    }
}