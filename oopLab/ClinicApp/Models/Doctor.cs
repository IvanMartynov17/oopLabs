using ClinicApp.Enums;
using ClinicApp.Utils;
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
            ClinicValidator.ValidateName(value, nameof(FirstName));
            _firstName = value;
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            ClinicValidator.ValidateName(value, nameof(FirstName));
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
            ClinicValidator.ValidatePhone(value);
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