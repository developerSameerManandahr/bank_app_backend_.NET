using System.ComponentModel.DataAnnotations;

namespace worksheet2.Model.Request
{
    /**
     * Request model that is required while authenticating with password
     */
    public class AuthenticateRequest
    {
        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }
    }
}