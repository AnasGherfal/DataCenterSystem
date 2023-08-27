using System.Text.RegularExpressions;

namespace Core.Helpers;

public static class Validation
{
    private static readonly Regex ArabicRegex = new Regex(@"^[\u0621-\u064A ]+$");
    private static readonly Regex EnglishRegex = new Regex(@"^[a-zA-Z ]+$");
    private static readonly Regex PhoneRegex = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
    private static readonly Regex CustomerPhoneRegex = new Regex(@"^\+(218\-?(0?)9[2?1?5?0?4?])\d{7}$");
    private static readonly Regex UserNameRegex = new Regex(@"^[a-zA-Z]{1}[a-zA-Z0-9\._\-]{0,23}[^.-]$", RegexOptions.Compiled);

    public static bool IsValidArabic(string? value)
    {
        if (value is null) return true;
        return ArabicRegex.IsMatch(value.Trim());
    }

    public static bool IsValidEnglish(string? value)
    {
        if (value is null) return true;
        return EnglishRegex.IsMatch(value.Trim());
    }

    public static bool IsValidPhoneNo(string? value)
    {
        if (value is null) return true;
        return PhoneRegex.IsMatch(value.Trim());
    }
    public static bool IsValidCustomerPhoneNo(string? value)
    {
        if (value is null) return true;
        return CustomerPhoneRegex.IsMatch(value.Trim());
    }

    public static bool IsValidLibyaMobile(string? value)
    {
        if (value is null) return true;
        var operatorCodes = new[] { "092", "094", "091", "093", "095", "096" };
        return value.Length == 10 && value.All(char.IsDigit) && operatorCodes.Any(value.StartsWith);
    }

    public static bool IsValidNationalNo(string? value)
    {
        if (value is null) return true;
        var (length, gender, nidYear) = (value.Length, int.Parse(value.Substring(0, 1)), int.Parse(value.Substring(1, 5)));
        return (length, gender, NIDYear: nidYear) switch
        {
            (12, 1 or 2, > 1800) => true,
            _ => false
        };
    }

    public static bool IsValidEmpCode(string? value)
    {
        if (value is null) return true;
        return value.Length == 4 && value.All(char.IsDigit);
    }

    public static bool IsValidNumber(string? value)
    {
        if (value is null) return true;
        return value.All(char.IsDigit);
    }

    public static bool IsValidAge(DateTime? date)
    {
        if (date is null) return true;
        int currentYear = DateTime.Now.Year;
        int birthdateYear = date.Value.Year;
        return birthdateYear <= currentYear && birthdateYear > (currentYear - 120);
    }

    public static bool IsValidEmploymentDate(DateTime? date)
    {
        if (date is null) return true;
        int currentYear = DateTime.Now.Year;
        int employmentYear = date.Value.Year;
        return employmentYear <= currentYear && employmentYear >= 1997;
    }

    public static bool IsValidTime(TimeSpan? time)
    {
        if (time is null) return true;
        return time < new TimeSpan(24, 0, 0);
    }

    public static bool IsValidUserName(string? value)
    {
        if (value is null) return true;
        return UserNameRegex.IsMatch(value.Trim());
    }
}
