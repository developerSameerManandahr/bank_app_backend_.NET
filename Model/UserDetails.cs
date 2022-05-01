using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace worksheet2.Model
{
    public class UserDetails
    {
        [Key] [ForeignKey("User")] public string UserUserDetailsId { get; set; }

        public virtual User User { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        
        public string Address { get; set; }
        
        public int PhoneNumber { get; set; }
    }
}