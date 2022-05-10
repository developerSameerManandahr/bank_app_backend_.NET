using System.ComponentModel.DataAnnotations;

namespace worksheet2.Model.Request
{
    public class SignupRequest
    {
        [Required] [MinLength(4)] public string Username { get; set; }

        [MinLength(8)] [Required] public string Password { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "The PIN should only contain number")]
        [MinLength(6)]
        [MaxLength(6)]
        [Required]
        public string Pin { get; set; }

        [Required] public string FirstName { get; set; }
        public string MiddleName { get; set; }

        [Required] public string LastName { get; set; }

        [Required] [MaxLength(50)] public string Address { get; set; }

        [MinLength(11)]
        [MaxLength(12)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "The Phone Number should consists number only")]
        [Required]
        public string PhoneNumber { get; set; }
    }
}