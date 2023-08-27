namespace Core.Exceptions;

public class FlowException: Exception
{
    public string Because { get; private set; }
    public object? Request { get; private set; }
    public FlowException(object? request, string because) : base("GenericInternalReport")
    {
        Request = request;
        Because = string.IsNullOrWhiteSpace(because) ? "N/A" : because;
    }
}