namespace worksheet2.Model.Response
{
    /**
     * Response seen after authenticating the user
     */
    public class AuthenticationResponse
    {
        public AuthenticationResponse(string firstName, string lastName, string userName, string token)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Token = token;
        }

        public AuthenticationResponse(User user, string token)
        {
            FirstName = user.UserDetails.FirstName;
            LastName = user.UserDetails.LastName;
            UserName = user.UserName;
            AccountNumber = user.AccountNumber;
            Token = token;
            MiddleName = user.UserDetails.MiddleName;
            Address = user.UserDetails.Address;
            PhoneNumber = user.UserDetails.PhoneNumber;
        }

        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public string AccountNumber { get; set; }
        public string Token { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Address { get; set; }
    }
}