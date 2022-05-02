using System.ComponentModel.DataAnnotations;

namespace worksheet2.Model.Request
{
    public class AuthenticatePinRequest
    {
        [Required]
        [MinLength(10)]
        [MaxLength(11)]
        public string AccountNumber { get; set; }

        [MinLength(6)]
        [MaxLength(6)]
        [RegularExpression("^[0-9]+$")]
        [Required] public string Pin { get; set; }
    }
}