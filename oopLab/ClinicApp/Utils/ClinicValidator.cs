namespace ClinicApp.Utils;

public class ClinicValidator
{
    public static void ValidateName(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
        {
            throw new ArgumentException("Не повинно бути порожнім або перевищувати 50 символів", fieldName);
        }
    }
    
    public static void ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone) || phone.Length != 10)
        {
            throw new ArgumentException("Номер телефону повинен містити 10 цифр", nameof(phone));
        }

        foreach (char c in phone)
        {
            if (!char.IsDigit(c))
            {
                throw new ArgumentException("Номер телефону повинен містити лише цифри", nameof(phone));
            }
        }
    }
    
    public static void ValidateDate(DateTime value, string fieldName)
    {
        if (value > DateTime.Today)
        {
            throw new ArgumentOutOfRangeException(fieldName, "Дата не може бути в майбутньому");
        }

        if (value.Year < 1900)
        {
            throw new ArgumentOutOfRangeException(fieldName, "Рік не може бути раніше 1900");
        }
    }
    
    public static void ValidatePositive(int value, string fieldName)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(fieldName, "Значення не може бути менше 0");
        }
    }
}