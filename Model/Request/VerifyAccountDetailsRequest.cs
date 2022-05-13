namespace worksheet2.Model.Request
{
    /**
     * Model used to verify the user is valid or not
     */
    public class VerifyAccountDetailsRequest
    {
        public string AccountNumber { get; set; }
        public string FullName { get; set; }
    }
}