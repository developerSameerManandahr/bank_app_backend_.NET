using System.ComponentModel.DataAnnotations;

namespace worksheet2.Model.Request
{
    /**
     * Request model that is required while transferring balance to other
     */
    public class PayRequest
    {
        public ToCredentials To { get; set; }
        public long Amount { get; set; }
        
        public string Description { get; set; }
        
        public string Pin { get; set; }
        
    }

    public class ToCredentials
    {
        public string FullName { get; set; }

        [MinLength(10)]
        [MaxLength(10)]
        public string AccountNumber { get; set; }
    }
}