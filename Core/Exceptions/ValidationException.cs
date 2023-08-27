using System.Globalization;

namespace Core.Exceptions;

public class ValidationException: Exception
{
    public IList<string> Errors { get; }
    public ValidationException(IList<string> errors, params object[] args) 
        : base(string.Format(CultureInfo.CurrentCulture, errors.Any() ? errors.First() : "", args))
    {
        Errors = errors;
    }
}