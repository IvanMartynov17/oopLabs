using ClinicApp.Models;

namespace ClinicApp.Managers;

public class MedicalRecordManager
{
    private readonly MedicalRecord[] _medicalRecords = new MedicalRecord[1000];
    private int _count = 0;
    
    public int Count => _count;
    
    public MedicalRecord? this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                return null;
            }
            return _medicalRecords[index];
        }
    }
    
    public void Add(MedicalRecord record)
    {
        if (record == null)
        {
            Console.WriteLine("Помилка: спроба додати порожній запис (null).");
            return;
        }

        if (_count >= _medicalRecords.Length)
        {
            Console.WriteLine("Помилка: досягнуто ліміт записів (1000). Неможливо додати новий запис.");
            return;
        }

        _medicalRecords[_count] = record;
        _count++;

        Console.WriteLine($"Запис [{record.Id}] {record.GetRecordType()} додано.");
    }
    
    public MedicalRecord? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_medicalRecords[i].Id == id)
            {
                return _medicalRecords[i];
            }
        }
        return null;
    }
    
    public MedicalRecord[] GetByPatient(int patientId)
    {
        List<MedicalRecord> result = new List<MedicalRecord>();
        for (int i = 0; i < _count; i++)
        {
            if (_medicalRecords[i].PatientId == patientId)
            {
                result.Add(_medicalRecords[i]);
            }
        }
        return result.ToArray();
    }
    
    public void DisplayList(MedicalRecord[] records)
    {
        if (records == null || records.Length == 0)
        {
            Console.WriteLine("Записи відсутні.");
            return;
        }

        foreach (var record in records)
        {
            Console.WriteLine($"[{record.GetRecordType()}] ID: {record.Id} | Пацієнт ID: {record.PatientId} | Дата: {record.Date:yyyy-MM-dd}");
            Console.WriteLine($"  Зміст: {record.GetSummary()}");
            Console.WriteLine($"  Статус: {(record.IsActive() ? "Активний" : "Неактивний")}");
            Console.WriteLine(new string('-', 40));
        }
    }
    
    public void DisplayAll()
    {
        if (_count == 0)
        {
            Console.WriteLine("Записи відсутні.");
            return;
        }

        MedicalRecord[] currentRecords = new MedicalRecord[_count];
        Array.Copy(_medicalRecords, currentRecords, _count);
        DisplayList(currentRecords);
    }
}