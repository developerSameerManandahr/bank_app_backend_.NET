using System.ComponentModel.DataAnnotations;

namespace worksheet2.Model.Request
{
    public class AuthenticatePinRequest
    {
        [Required] public string AccountNumber { get; set; }

        [Required] public string Pin { get; set; }
    }
}