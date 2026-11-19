using System;
using System.Globalization;
using System.IO;
using System.Text;
using ClinicApp.Models;
using ClinicApp.Enums;

namespace ClinicApp.Utils;

public class SessionManager
{
    private readonly string _sessionPath;

    public SessionManager(string sessionPath = "session.dat")
    {
        if (string.IsNullOrWhiteSpace(sessionPath))
        {
            throw new ArgumentException("Шлях не може бути порожнім.", nameof(sessionPath));
        }
        
        _sessionPath = sessionPath;
    }

    public bool Exists()
    {
        return File.Exists(_sessionPath);
    }
    
    public void Save(Clinic clinic)
    {
        ArgumentNullException.ThrowIfNull(clinic);
        
        using StreamWriter writer = new StreamWriter(_sessionPath, false, Encoding.UTF8);
        writer.WriteLine("[PATIENTS]");
        Patient[] patients = clinic.Patients.GetAll();
        foreach (Patient p in patients)
        {
            writer.WriteLine($"{p.FirstName},{p.LastName},{p.DateOfBirth:dd.MM.yyyy},{p.BloodType},{p.Phone}");
        }
        writer.WriteLine("[END]");
    }

    public int Load(Clinic clinic)
    {
        ArgumentNullException.ThrowIfNull(clinic);

        if (!Exists())
        {
            return 0;
        }
        
        string[] lines = File.ReadAllLines(_sessionPath, Encoding.UTF8);
        string? currentSection = null;
        int loadedCount = 0;

        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (line.StartsWith('['))
            {
                if (line.Equals("[END]", StringComparison.OrdinalIgnoreCase))
                {
                    currentSection = null;
                }
                else
                {
                    currentSection = line.ToUpperInvariant();
                }
                continue;
            }

            if (currentSection != "[PATIENTS]")
            {
                continue;
            }

            try
            {
                string[] parts = line.Split(',');
                if (parts.Length != 5)
                {
                    continue;
                }

                string firstName = parts[0].Trim();
                string lastName = parts[1].Trim();
                DateTime dateOfBirth = DateTime.ParseExact(parts[2].Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                BloodType bloodType = (BloodType)Enum.Parse(typeof(BloodType), parts[3].Trim(), ignoreCase: true);
                string phone = parts[4].Trim();

                if (IsPatientExists(clinic, firstName, lastName, dateOfBirth))
                {
                    continue;
                }

                Patient patient = new Patient(
                    firstName,
                    lastName,
                    dateOfBirth,
                    FormatBloodTypeDisplay(bloodType),
                    bloodType,
                    phone,
                    string.Empty
                );

                clinic.Patients.Add(patient);
                loadedCount++;
            }
            catch (Exception)
            {
                
            }
        }
        return loadedCount;
    }
    
    private static bool IsPatientExists(Clinic clinic, string firstName, string lastName, DateTime dateOfBirth)
    {
        Patient[] existing = clinic.Patients.GetAll();
        foreach (Patient p in existing)
        {
            if (string.Equals(p.FirstName, firstName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(p.LastName, lastName, StringComparison.OrdinalIgnoreCase) &&
                p.DateOfBirth.Date == dateOfBirth.Date)
            {
                return true;
            }
        }

        return false;
    }
    
    private static string FormatBloodTypeDisplay(BloodType bloodType)
    {
        return bloodType switch
        {
            BloodType.APositive => "A+",
            BloodType.ANegative => "A-",
            BloodType.BPositive => "B+",
            BloodType.BNegative => "B-",
            BloodType.ABPositive => "AB+",
            BloodType.ABNegative => "AB-",
            BloodType.OPositive => "O+",
            BloodType.ONegative => "O-",
            _ => bloodType.ToString()
        };
    }
}