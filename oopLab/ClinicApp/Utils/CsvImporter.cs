using System.Globalization;
using System.Text;
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public static class CsvImporter
{
    public static ImportResult ImportPatients(Clinic clinic, string filePath)
    {
        ArgumentNullException.ThrowIfNull(clinic);

        ImportResult result = new ImportResult();

        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            result.AddError(0, $"Файл не знайдено: {filePath}");
            return result;
        }

        string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

        for (int i = 1; i < lines.Length; i++)
        {
            int lineNumber = i + 1;
            string line = lines[i];

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            try
            {
                string[] parts = line.Split(',');
                if (parts.Length != 5)
                {
                    string word = parts.Length == 1 ? "поле" : (parts.Length < 5 ? "поля" : "полів");
                    result.AddError(lineNumber, $"Рядок має {parts.Length} {word} замість 5.");
                    continue;
                }

                string firstName = parts[0].Trim();
                string lastName = parts[1].Trim();
                string dobStr = parts[2].Trim();
                string bloodTypeStr = parts[3].Trim();
                string phone = parts[4].Trim();

                DateTime dateOfBirth = DateTime.ParseExact(dobStr, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                BloodType bloodType = (BloodType)Enum.Parse(typeof(BloodType), bloodTypeStr, ignoreCase: true);

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
                clinic.Logger.LogInfo($"Імпортовано пацієнта #{patient.Id}: {patient.FullName}");
                result.AddSuccess();
            }
            catch (FormatException)
            {
                result.AddError(lineNumber, "Некоректний формат дати (очікується dd.MM.yyyy).");
            }
            catch (ArgumentException ex)
            {
                result.AddError(lineNumber, ex.Message);
            }
            catch (Exception ex)
            {
                result.AddError(lineNumber, ex.Message);
            }
        }

        return result;
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