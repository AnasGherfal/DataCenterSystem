namespace Infrastructure.Constants;

public enum RegexValidation
{
    PhoneNumber,
}

public static class RegexValidationExtension
{
    public static string Rule(this RegexValidation regexValidation) 
        => regexValidation switch 
        {
            RegexValidation.PhoneNumber => "^(\\+2189+[1,2,4,5])+[0-9]{7}$",
            _ => throw new NotImplementedException(),
        };
}