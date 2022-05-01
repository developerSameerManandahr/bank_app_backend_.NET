using System.ComponentModel.DataAnnotations;

namespace worksheet2.Model.Request
{
    public class SignupRequest
    {
        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }

        [Required] public string Pin { get; set; }

        [Required] public string FirstName { get; set; }
        [Required] public string MiddleName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string Address { get; set; }
        [Required] public int PhoneNumber { get; set; }
    }
}