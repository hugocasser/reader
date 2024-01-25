namespace DataAccessLayer.Exeptions;

public class UserSecretsInvalidException : Exception
{
    public UserSecretsInvalidException(string message) 
        : base(message)
    {
    }
}