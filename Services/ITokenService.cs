namespace worksheet2.Services
{
    public interface ITokenService
    {
        /**
         * Validates the token provided
         */
        bool ValidateToken(string token);
        
        /**
         * Checks the token for user id 
         */
        string GetUserIdFromToken(string token);
    }
}