using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Prescription : MedicalRecord
{
    private string _medicationName = string.Empty;
    private string _dosage = string.Empty;
    private int _durationDays = 0;
    
    public string MedicationName {
        get => _medicationName;
        set
        {
            ClinicValidator.ValidateName(value, nameof(MedicationName)); 
            _medicationName = value;
        }
    }
    
    public string Dosage {
        get => _dosage;
        set
        {
            ClinicValidator.ValidateName(value, nameof(Dosage));
            _dosage = value;
        }
    }

    public int DurationDays
    {
        get => _durationDays;
        set
        {
            ClinicValidator.ValidatePositive(value, nameof(DurationDays));
            _durationDays = value;
        }
    }
    
    public string Instructions { get; set; } = string.Empty;

    public DateTime ExpiresAt
    {
        get => Date.AddDays(DurationDays);
        
    }
    
    public Prescription(int patientId, int doctorId, DateTime date, string medicationName, string dosage, int durationDays, string instructions = "")
        : base(patientId, doctorId, date)
    {
        MedicationName = medicationName;
        Dosage = dosage;
        DurationDays = durationDays;
        Instructions = instructions;
    }
    
    public override string GetSummary()
    {
        if (string.IsNullOrWhiteSpace(Instructions))
        {
            return $"{MedicationName} {Dosage} × {DurationDays} днів";
        }

        return $"{MedicationName} {Dosage} × {DurationDays} днів ({Instructions})";
    }
    
    public override string GetRecordType()
    {
        return "Рецепт";
    }

    public override bool IsActive()
    {
        return ExpiresAt >= DateTime.Today;
    }
}