namespace worksheet2.Model.Response
{
    /**
     * Response seen from account details API
     */
    public class AccountDetailsResponse
    {
        public long Balance { get; set; }
        public string AccountType { get; set; }
        public string Currency { get; set; }
    }
}