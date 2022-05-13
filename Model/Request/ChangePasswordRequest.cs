namespace worksheet2.Model.Request
{
    /**
     * Request model that is required while sending password change request from API
     */
    public class ChangePasswordRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}