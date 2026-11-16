using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils;

public static class ModelValidator
{
    public static ValidationResult Validate(object obj)
    {
        ArgumentNullException.ThrowIfNull(obj);

        var result = new ValidationResult();
        Type type = obj.GetType();
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (PropertyInfo prop in properties)
        {
            object? value = prop.GetValue(obj);

            var requiredAttr = prop.GetCustomAttribute<RequiredAttribute>();
            if (requiredAttr is not null)
            {
                bool isInvalid = value is null || (value is string str && string.IsNullOrWhiteSpace(str));
                if (isInvalid)
                {
                    result.AddError($"{prop.Name}: {requiredAttr.ErrorMessage}");
                }
            }

            var maxLengthAttr = prop.GetCustomAttribute<MaxLengthAttribute>();
            if (maxLengthAttr is not null && value is string text)
            {
                if (text.Length > maxLengthAttr.Length)
                {
                    string message = string.IsNullOrEmpty(maxLengthAttr.ErrorMessage)
                        ? $"Maximum length is {maxLengthAttr.Length}."
                        : maxLengthAttr.ErrorMessage;
                    result.AddError($"{prop.Name}: {message}");
                }
            }

            var minValueAttr = prop.GetCustomAttribute<MinValueAttribute>();
            if (minValueAttr is not null && value is not null && IsNumeric(value))
            {
                double numericValue = Convert.ToDouble(value);
                if (numericValue < minValueAttr.Min)
                {
                    string message = string.IsNullOrEmpty(minValueAttr.ErrorMessage)
                        ? $"Value must be at least {minValueAttr.Min}."
                        : minValueAttr.ErrorMessage;
                    result.AddError($"{prop.Name}: {message}");
                }
            }
        }

        return result;
    }

    public static void PrintInfo(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        Console.WriteLine($"Модель: {type.Name}");
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (PropertyInfo prop in properties)
        {
            var attrNames = new List<string>();

            var req = prop.GetCustomAttribute<RequiredAttribute>();
            if (req is not null) attrNames.Add("[Required]");

            var max = prop.GetCustomAttribute<MaxLengthAttribute>();
            if (max is not null) attrNames.Add($"[MaxLength({max.Length})]");

            var min = prop.GetCustomAttribute<MinValueAttribute>();
            if (min is not null) attrNames.Add($"[MinValue({min.Min})]");

            string attrsText = attrNames.Count > 0
                ? " — " + string.Join(", ", attrNames)
                : "";

            Console.WriteLine($"  {prop.Name} ({prop.PropertyType.Name}){attrsText}");
        }
    }

    private static bool IsNumeric(object value)
    {
        return value is sbyte or byte or short or ushort
            or int or uint or long or ulong
            or float or double or decimal;
    }
}