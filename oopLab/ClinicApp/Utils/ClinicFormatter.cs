using ClinicApp.Models;
using ClinicApp.Enums;

namespace ClinicApp.Utils;

public static class ClinicFormatter
{
    public static string FormatBloodType(BloodType bT)
    {

        return bT switch
        {
            BloodType.APositive => "A+",
            BloodType.ANegative => "A-",
            BloodType.BPositive => "B+",
            BloodType.BNegative => "B-",
            BloodType.ABPositive => "AB+",
            BloodType.ABNegative => "AB-",
            BloodType.OPositive => "O+",
            BloodType.ONegative => "O-",
            _ => "Невідома група крові"
        };

    }
    
    public static string FormatSpeciality(Speciality s)
    {
        return s switch
        {
            Speciality.Cardiology => "Кардіологія",
            Speciality.Neurology => "Неврологія",
            Speciality.Orthopedics => "Ортопедія",
            Speciality.Dermatology => "Дерматологія",
            Speciality.Emergency => "Швидка допомога",
            Speciality.General => "Загальна терапія",
            Speciality.Pediatrics => "Педіатрія",
            Speciality.Surgery => "Хірургія",
            _=> "Невідома спеціальність"
        };
    }
    
    public static string FormatAge(int age)
    {
        int mod100 = age % 100;
        int mod10 = age % 10;

        if (mod100 >= 11 && mod100 <= 19)
        {
            return $"{age} років";
        }

        return mod10 switch
        {
            1 => $"{age} рік",
            2 or 3 or 4 => $"{age} роки",
            _ => $"{age} років"
        };
    }
    public static string FormatPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone)) return "Телефон відсутній";

        char[] digits = phone.Where(char.IsDigit).ToArray();
        if (digits.Length > 10)
        {
            digits = digits.TakeLast(10).ToArray();
        }

        if (digits.Length == 10)
        {
            string d = new string(digits);
            return $"({d[..3]}) {d[3..6]}-{d[6..]}";
        }

        return phone;
    }
}