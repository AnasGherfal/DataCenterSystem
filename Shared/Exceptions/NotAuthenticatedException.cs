namespace Shared.Exceptions;

public class NotAuthenticatedException: Exception
{
    public string Because { get; private set; }
    public NotAuthenticatedException(string because) : base( "Not Authenticated")
    {
        Because = string.IsNullOrWhiteSpace(because) ? "N/A" : because;
    }
}