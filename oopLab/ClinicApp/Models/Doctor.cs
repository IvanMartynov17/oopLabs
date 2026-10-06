using ClinicApp.Enums;

namespace ClinicApp.Models;
public class Doctor
{
    private static int _nextID = 1;

    public int Id { get; }
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private string _licenseNumber = string.Empty;
    private string _phone = string.Empty;

    public string FirstName
    {
        get => _firstName;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
            {
                throw new ArgumentException("Ім'я не може бути порожнім або довшим за 50 символів.", nameof(FirstName));
            }
            _firstName = value;
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
            {
                throw new ArgumentException("Прізвище не може бути порожнім або довшим за 50 символів.", nameof(LastName));
            }
            _lastName = value;
        }
    }

    public string LicenseNumber
    {
        get => _licenseNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Номер ліцензії не може бути порожнім.", nameof(LicenseNumber));
            }
            _licenseNumber = value;
        }
    }

    public string Phone
    {
        get => _phone;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 10)
            {
                throw new ArgumentException("Номер телефону повинен містити рівно 10 цифр.", nameof(Phone));
            }
            foreach (char c in value)
            {
                if (!char.IsDigit(c))
                {
                    throw new ArgumentException("Номер телефону повинен містити лише цифри.", nameof(Phone));
                }
            }
            _phone = value;
        }
    }

    public Speciality Specialty;
    public WorkSchedule Schedule;

    public string FullName => $"{FirstName} {LastName}";
    public bool IsAvailableNow => Schedule.IsNow;

    public int WorkingHoursPerDay => Schedule.HoursPerDay;
    public Doctor(
        string firstName, 
        string lastName, 
        Speciality specialty, 
        string licenseNumber, 
        string phone, 
        WorkSchedule schedule)
    {
        Id = _nextID++;
        FirstName = firstName;
        LastName = lastName;
        Specialty = specialty;
        LicenseNumber = licenseNumber;
        Phone = phone;
        Schedule = schedule;
    }
    public Doctor(
        string firstName, 
        string lastName, 
        Speciality specialty, 
        string licenseNumber, 
        string phone, 
        int workStartHour, 
        int workEndHour) 
        : this(firstName, lastName, specialty, licenseNumber, phone, new WorkSchedule(workStartHour, workEndHour))
    {
    }

    public Doctor(string firstName, string lastName, Speciality specialty, string licenseNumber, string phone)
        : this(firstName, lastName, specialty, licenseNumber, phone, 9, 17)
    {
    }

    public Doctor(string firstName, string lastName, Speciality specialty)
        : this(firstName, lastName, specialty, "Невідомо", "Невідомо", 9, 17)
    {
    }

    public bool CanAcceptAt(int hour)
    {
        return Schedule.Contains(hour);
    }

    public override string ToString()
    {
        string status = IsAvailableNow ? "Доступний зараз" : "Не в робочий час";
        return $"[{Id}] {FullName} | {Specialty} | {LicenseNumber} | {Phone} | {Schedule} ({Schedule.HoursPerDay} год) | {status}";
    }
}