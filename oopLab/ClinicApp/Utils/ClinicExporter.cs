using System;
using System.IO;
using System.Text;
using ClinicApp.Interfaces;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class ClinicExporter
{
    private readonly Clinic _clinic;
    private readonly string _baseDir;

    public ClinicExporter(Clinic clinic, string baseDir = "reports")
    {
        ArgumentNullException.ThrowIfNull(clinic);
        if (string.IsNullOrWhiteSpace(baseDir))
        {
            throw new ArgumentException("Базова тека не може бути порожньою.", nameof(baseDir));
        }
        _clinic = clinic;
        _baseDir = baseDir;
    }

    public string ExportPatients()
    {
        Patient[] patients = _clinic.Patients.GetAll();
        string[] lines = new string[patients.Length];
        for (int i = 0; i < patients.Length; i++)
        {
            lines[i] = patients[i].ToString();
        }
        return WriteReport("patients.txt", "Пацієнти — звіт", lines, $"Всього: {patients.Length}");
    }
    
    public string ExportAppointments()
    {
        Appointment[] appointments = _clinic.Appointments.GetAll();
        string[] lines = new string[appointments.Length];
        for (int i = 0; i < appointments.Length; i++)
        {
            lines[i] = appointments[i].ToString();
        }

        return WriteReport("appointments.txt", "Записи на прийом — звіт", lines, $"Всього: {appointments.Length}");
    }

    public string ExportBilling()
    {
        IPayable[] unpaid = _clinic.Billing.GetAllUnpaid();
        decimal totalDept = _clinic.Billing.GetTotalDebt();

        string[] lines = new string[unpaid.Length];
        for (int i = 0; i < unpaid.Length; i++)
        {
            lines[i] = unpaid[i].ToString() ?? "";
        }
        string footer = $"Всього неоплачених: {unpaid.Length} | Загальний борг: {totalDept:F2} грн";
        return WriteReport("billing.txt", "Неоплачені рахунки — звіт", lines, footer);
    }

    public string ExportTreatmentPlans()
    {
        TreatmentPlan[] plans = _clinic.TreatmentPlans.GetAll();
        string[] lines = new string[plans.Length];
        for (int i = 0; i < plans.Length; i++)
        {
            lines[i] = plans[i].ToString();
        }

        return WriteReport("treatment_plans.txt", "Плани лікування — звіт", lines, $"Всього: {plans.Length}");
    }
    
    public string[] ExportAll()
    {
        return new[]
        {
            ExportPatients(),
            ExportAppointments(),
            ExportBilling(),
            ExportTreatmentPlans()
        };
    }
    
    private string PrepareDailyDirectory()
    {
        string dateFolder = DateTime.Now.ToString("yyyy-MM-dd");
        string fullDir = Path.Combine(_baseDir, dateFolder);
        Directory.CreateDirectory(fullDir);
        return fullDir;
    }
    
    private string WriteReport(string fileName, string title, string[] items, string footer)
    {
        string dir = PrepareDailyDirectory();
        string filePath = Path.Combine(dir, fileName);
        string separator = new string('-', 40);

        using StreamWriter writer = new StreamWriter(filePath, append: false, Encoding.UTF8);

        writer.WriteLine($"=== {title} ===");
        writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
        writer.WriteLine(separator);

        if (items.Length == 0)
        {
            writer.WriteLine("  Дані відсутні.");
        }
        else
        {
            for (int i = 0; i < items.Length; i++)
            {
                writer.WriteLine($"{i + 1,3}. {items[i]}");
            }
        }

        writer.WriteLine(separator);
        writer.WriteLine(footer);

        return filePath;
    }
}