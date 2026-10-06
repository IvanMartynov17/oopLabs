using ClinicApp.Enums;

namespace ClinicApp.Models;
public class Patient
{
    private static int _nextID = 1;
    public int Id { get; }
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private  string _phone = string.Empty;
    private DateTime _dateOfBirth;

    public string FirstName
    {
        get => _firstName; 
        set => _firstName = value;
    }

    public string LastName
    {
        get => _lastName;
        set => _lastName = value;
    }

    public string Phone
    {
        get => _phone;
        set => _phone = value;
    }

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set => _dateOfBirth = value;
    }

    
    public  BloodType BloodType { get; set; }
    public  string Email { get; set; }
    public string FullName
    {
        get
        {
            return $"{FirstName}, {LastName}";
        }
    }

    public bool IsAdult
    {
        get
        {
            return Age >= 18;
        }
    }

    public int Age
    {
        get
        {
            DateTime today = DateTime.Today;
            int age = today.Year - DateOfBirth.Year;
            if (DateOfBirth > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }
   

    public Patient(string firstName, string lastName, DateTime dateOfBirth, string s, BloodType bloodType, string phone,
        string email)
    {
        Id = _nextID++;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        BloodType = bloodType;
        Phone = phone;
        Email = email;
    }

    public Patient(string firstName, string lastName, DateTime dateOfBirth, string bloodType, string phone)
        : this(firstName, lastName, dateOfBirth, "Невідомо", BloodType.Unknown, phone, string.Empty)
    {
    }
    public Patient(string firstName, string lastName, DateTime dateOfBirth)
    {
    }
    public string GetAgeCategory()
    {
        if (Age < 18)
        {
            return "Дитина";
        }else if (Age < 60)
        {
            return "Дорослий";
        }
        else
        {
            return "Лiтнiй";
        }
    }

    public override string ToString()
    {
        return $"[{Id}] {FirstName} {LastName} | Вiк: {Age} ({GetAgeCategory()}) | Кров: {BloodType} | Тел: {Phone} \n";
    }
}