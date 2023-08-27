using System.ComponentModel;

namespace Core.Helpers;

public static class EnumExtension
{
    public static string KeyName(this Enum value)
    {
        var type = value.GetType();
        var res = new List<string>();
        var arrValue = value.ToString().Split(',').Select(v => v.Trim());
        foreach (var strValue in arrValue)
        {
            var memberInfo = type.GetMember(strValue);
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs.Length > 0 && attrs.FirstOrDefault(t => t.GetType() == typeof(DescriptionAttribute)) != null)
                {
                    res.Add((((DescriptionAttribute)attrs
                        .FirstOrDefault(t => t.GetType() == typeof(DescriptionAttribute))!)!).Description);
                }
                else
                    res.Add(strValue);
            }
            else
                res.Add(strValue);
        }
        return res.Aggregate((s,v) => s + ", " + v);
    }
}