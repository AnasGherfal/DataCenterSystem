namespace Core.Validators;

public enum RegexValidation
{
    PhoneNumber,
    Arabic,
    English,
    UserName,
}

public static class RegexValidationExtension
{
    public static string Rule(this RegexValidation regexValidation) 
        => regexValidation switch 
        {
            RegexValidation.PhoneNumber => "^(\\+2189+[1,2,4,5])+[0-9]{7}$",
            RegexValidation.Arabic => @"^[\u0621-\u064A ]+$",
            RegexValidation.English => @"^[a-zA-Z ]+$",
            RegexValidation.UserName => @"^[a-zA-Z]{1}[a-zA-Z0-9\._\-]{0,23}[^.-]$",
            _ => throw new NotImplementedException(),
        };
}