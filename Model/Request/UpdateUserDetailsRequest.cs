using System.ComponentModel.DataAnnotations;

namespace worksheet2.Model.Request
{
    /**
     * Request data required while updating user details 
     */
    public class UpdateUserDetailsRequest
    {
        [Required] [MaxLength(50)] public string Address { get; set; }

        [MinLength(11)]
        [MaxLength(12)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "The Phone Number should consists number only")]
        [Required]
        public string PhoneNumber { get; set; }
    }
}