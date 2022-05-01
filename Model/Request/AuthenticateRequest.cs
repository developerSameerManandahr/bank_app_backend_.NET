using System.ComponentModel.DataAnnotations;

namespace worksheet2.Model.Request
{
    public class AuthenticateRequest
    {
        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }
    }
}