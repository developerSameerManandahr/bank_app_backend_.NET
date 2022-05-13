namespace worksheet2.Model.Request
{
    /**
     * Request model that is required while sending PIN change request from API
     */
    public class ChangePinRequest
    {
        public string OldPin { get; set; }
        public string NewPin { get; set; }
    }
}