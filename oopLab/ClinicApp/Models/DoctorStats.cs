namespace ClinicApp.Models;

public class DoctorStats : IComparable<DoctorStats>
{
    public int DoctorId { get; }
    public string FullName { get; }
    public int AppointmentCount { get; }
    public decimal TotalRevenue { get; }
    public DateTime LastAppointmentDate { get; }

    public DoctorStats(int doctorId, string fullName, int appointmentCount,
        decimal totalRevenue, DateTime lastAppointmentDate)
    {
        DoctorId = doctorId;
        FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
        AppointmentCount = appointmentCount;
        TotalRevenue = totalRevenue;
        LastAppointmentDate = lastAppointmentDate;
    }

    public int CompareTo(DoctorStats? other)
    {
        if (other is null) return -1;
        
        return other.AppointmentCount.CompareTo(AppointmentCount);
    }

    public override string ToString()
    {
        string lastDate = LastAppointmentDate == DateTime.MinValue ? "немає прийомів" 
            : LastAppointmentDate.ToString("dd.MM.yyyy");
        return $"ID: {DoctorId} | {FullName} | Прийомів: {AppointmentCount} | Виручка: {TotalRevenue:C} | Останній прийом: {lastDate}";
    }
}