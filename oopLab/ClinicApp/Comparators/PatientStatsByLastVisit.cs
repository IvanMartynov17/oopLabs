using ClinicApp.Models;

namespace ClinicApp.Comparators;

public class PatientStatsByLastVisit : IComparer<PatientStats>
{
    public int Compare(PatientStats? x, PatientStats? y)
    {
        if(ReferenceEquals(x, y)) return 0;
        if(x is null) return 1;
        if(y is null) return -1;

        return x.LastVisitDate.CompareTo(y.LastVisitDate);
    }
}