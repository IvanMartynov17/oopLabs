using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using ClinicApp.Attributes;

namespace ClinicApp.Utils;

public static class FormBuilder
{
    public static T Build<T>() where T : new()
    {
        T obj = new T();
        Type type = typeof(T);
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (PropertyInfo prop in properties)
        {
            if (prop.GetSetMethod() is null)
            {
                continue;
            }

            string prompt = BuildPrompt(prop);

            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? "";

                try
                {
                    object? convertedValue;

                    if (prop.PropertyType.IsEnum)
                    {
                        convertedValue = Enum.Parse(prop.PropertyType, input, ignoreCase: true);
                    }
                    else
                    {
                        convertedValue = Convert.ChangeType(input, prop.PropertyType, CultureInfo.InvariantCulture);
                    }

                    prop.SetValue(obj, convertedValue);
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Некоректне значення, спробуйте ще раз");
                }
            }
        }

        return obj;
    }
    private static string BuildPrompt(PropertyInfo prop)
    {
        var hints = new List<string>();

        if (prop.GetCustomAttribute<RequiredAttribute>() is not null)
        {
            hints.Add("обов'язкове");
        }

        var maxLengthAttr = prop.GetCustomAttribute<MaxLengthAttribute>();
        if (maxLengthAttr is not null)
        {
            hints.Add($"макс. {maxLengthAttr.Length}");
        }

        var minValueAttr = prop.GetCustomAttribute<MinValueAttribute>();
        if (minValueAttr is not null)
        {
            hints.Add($"мін. {minValueAttr.Min}");
        }

        if (hints.Count > 0)
        {
            return $"  {prop.Name} ({string.Join(", ", hints)}): ";
        }

        return $"  {prop.Name}: ";
    }
}