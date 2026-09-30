namespace Lab1;

public class Doctor
{
    private static int _nextID = 1;

    public int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Speciality Specialty { get; set; }
    public string LicenseNumber { get; set; }
    public string Phone { get; set; }
    public WorkSchedule Schedule { get; set; }

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