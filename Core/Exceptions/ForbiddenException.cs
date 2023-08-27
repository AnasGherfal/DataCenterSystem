namespace Core.Exceptions;

public class ForbiddenException: Exception
{
    public string Because { get; private set; }
    public ForbiddenException(string because) : base("Forbidden")
    {
        Because = string.IsNullOrWhiteSpace(because) ? "N/A" : because;
    }
}