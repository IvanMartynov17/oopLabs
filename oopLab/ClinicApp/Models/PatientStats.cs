using System.Globalization;
using System;
namespace ClinicApp.Models;

public class PatientStats : IComparable<PatientStats>
{   
    public int PatientId { get; }
    public string FullName { get; }
    public int VisitCount { get; }
    public decimal TotalSpent { get; }
    public DateTime LastVisitDate { get; }

    public PatientStats(int patientId, string fullName, int visitCount, 
        decimal totalSpent, DateTime lastVisitDate)
    {
        PatientId = patientId;
        FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
        VisitCount = visitCount;
        TotalSpent = totalSpent;
        LastVisitDate = lastVisitDate;
    }

    public int CompareTo(PatientStats? other)
    {
        if (other is null) return -1;
        return other.VisitCount.CompareTo(VisitCount);
    }

    public override string ToString()
    {
        string lastVisit = LastVisitDate == DateTime.MinValue ? "—" : LastVisitDate.ToString("dd.MM.yyyy");
        string formattedSpent = TotalSpent.ToString("F2", CultureInfo.InvariantCulture);
        return $"[{PatientId}] {FullName} | Візитів: {VisitCount} | Витрачено: {formattedSpent} грн | Останній візит: {lastVisit}";
    }
}