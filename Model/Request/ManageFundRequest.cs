namespace worksheet2.Model.Request
{
    public class ManageFundRequest
    {
        public AccountType fromAccountType { get; set; }
        public AccountType toAccountType { get; set; }
        public long amount { get; set; }
    }
}