namespace Lab1;

public class Doctor
{
    private static int _nextID = 1;
    public int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Specialty { get; set; }
    public string LicenseNumber { get; set; }
    public string Phone { get; set; }
    public int WorkStartHour { get; set; }
    public int WorkEndHour { get; set; }
    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    public int WorkingHoursPerDay
    {
        get
        {
            return WorkEndHour - WorkStartHour;
        }   
    }

    public string WorkSchedule
    {
        get
        {
            return $"{WorkStartHour} - {WorkEndHour}";
        }
    }

    public bool IsAvailableNow => CanAcceptAt(DateTime.Now.Hour);
    public Doctor(string firstName, string lastName, string specialty, string licenceNumber, 
        string phone, int workStartHour, int workEndHour)
    {
        Id = _nextID++;
        FirstName = firstName;
        LastName = lastName;
        Specialty = specialty;
        LicenseNumber = licenceNumber;
        Phone = phone;
        WorkStartHour = workStartHour;
        WorkEndHour = workEndHour;
    }
    public Doctor(string firstName, string lastName, string specialty, string licenceNumber, string phone)
        : this(firstName, lastName, specialty, licenceNumber, phone, 9, 17)
    {
        
    }

    public Doctor(string firstName, string lastName, string specialty)
        : this(firstName, lastName, specialty, "Невідомо", "Невідомо", 9, 17)
    {
    }

    public bool CanAcceptAt(int hour)
    {
        return hour >= WorkStartHour && hour < WorkEndHour;
    }
    
    public override string ToString()
    {
        string status = IsAvailableNow ? "Доступний зараз" : "Не в робочий час";
        return $"[{Id}] {FirstName} {LastName} | {Specialty} " +
               $"| {LicenseNumber} |  {Phone} | {WorkSchedule} {WorkingHoursPerDay} год | {status}";
    }
}