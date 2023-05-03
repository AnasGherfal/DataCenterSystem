using System.Globalization;

namespace Shared.Exceptions;

public class ValidationException: Exception
{
    public IList<string> Errors { get; }
    public ValidationException(IList<string> errors, params object[] args) 
        : base(string.Format(CultureInfo.CurrentCulture, errors.Any() ? errors.First() : "", args))
    {
    }
}