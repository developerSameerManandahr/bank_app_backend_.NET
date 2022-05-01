﻿namespace worksheet2.Model.Response
{
    public class AuthenticationResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public string AccountNumber { get; set; }
        public string Token { get; set; }

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
        }
    }
}