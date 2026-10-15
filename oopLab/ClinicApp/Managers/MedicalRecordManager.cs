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

    public Diagnosis[] GetDiagnosis(int patientId)
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_medicalRecords[i].PatientId == patientId && _medicalRecords[i] is Diagnosis)
            {
                matchCount++;
            }
        }
        Diagnosis[] result = new Diagnosis[matchCount];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_medicalRecords[i].PatientId == patientId && _medicalRecords[i] is Diagnosis diag)
            {
                result[index++] = diag;
            }
        }
        return result;
    }
    
    public LabResult[] GetLabResults(int patientId)
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_medicalRecords[i].PatientId == patientId && _medicalRecords[i] is LabResult)
            {
                matchCount++;
            }
        }

        LabResult[] result = new LabResult[matchCount];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_medicalRecords[i].PatientId == patientId && _medicalRecords[i] is LabResult lab)
            {
                result[index++] = lab;
            }
        }
        return result;
    }
    
    public Prescription[] GetPrescriptions(int patientId)
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_medicalRecords[i].PatientId == patientId && _medicalRecords[i] is Prescription)
            {
                matchCount++;
            }
        }

        Prescription[] result = new Prescription[matchCount];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_medicalRecords[i].PatientId == patientId && _medicalRecords[i] is Prescription presc)
            {
                result[index++] = presc;
            }
        }
        return result;
    }
    
    public Diagnosis[] GetChronicDiagnoses(int patientId)
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_medicalRecords[i].PatientId == patientId && _medicalRecords[i] is Diagnosis diag && diag.IsChronic)
            {
                matchCount++;
            }
        }

        Diagnosis[] result = new Diagnosis[matchCount];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_medicalRecords[i].PatientId == patientId && _medicalRecords[i] is Diagnosis diag && diag.IsChronic)
            {
                result[index++] = diag;
            }
        }
        return result;
    }
    
    public Prescription[] GetActivePrescriptions(int patientId)
    {
        int matchCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_medicalRecords[i].PatientId == patientId && _medicalRecords[i] is Prescription presc && presc.IsActive())
            {
                matchCount++;
            }
        }

        Prescription[] result = new Prescription[matchCount];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_medicalRecords[i].PatientId == patientId && _medicalRecords[i] is Prescription presc && presc.IsActive())
            {
                result[index++] = presc;
            }
        }
        return result;
    }
    public void DisplayPatientSummary(int patientId)
    {
        MedicalRecord[] patientRecords = GetByPatient(patientId);

        if (patientRecords.Length == 0)
        {
            Console.WriteLine($"У пацієнта #{patientId} немає жодних медичних записів.");
            return;
        }

        int diagnosisCount = 0;
        int labResultCount = 0;
        int prescriptionCount = 0;

        foreach (var record in patientRecords)
        {
            if (record is Diagnosis) diagnosisCount++;
            else if (record is LabResult) labResultCount++;
            else if (record is Prescription) prescriptionCount++;
        }

        Console.WriteLine($"=== Медична картка пацієнта #{patientId} ===");
        Console.WriteLine($"Всього записів: {patientRecords.Length} (діагнозів: {diagnosisCount}, аналізів: {labResultCount}, рецептів: {prescriptionCount})");

        Diagnosis[] chronicDiagnoses = GetChronicDiagnoses(patientId);
        if (chronicDiagnoses.Length > 0)
        {
            Console.WriteLine($"Хронічні діагнози ({chronicDiagnoses.Length}):");
            foreach (var diag in chronicDiagnoses)
            {
                Console.WriteLine($"  [{diag.Id}] {diag.GetRecordType()} | {diag.Date:dd.MM.yyyy} | {diag.GetSummary()}");
            }
        }

        Prescription[] activePrescriptions = GetActivePrescriptions(patientId);
        if (activePrescriptions.Length > 0)
        {
            Console.WriteLine($"Активні рецепти ({activePrescriptions.Length}):");
            foreach (var presc in activePrescriptions)
            {
                Console.WriteLine($"  [{presc.Id}] {presc.GetRecordType()} | {presc.Date:dd.MM.yyyy} | {presc.GetSummary()} | до {presc.Date:dd.MM.yyyy}");
            }
        }
    }

    public MedicalRecord[] GetByDoctorId(int doctorId)
    {
        throw new NotImplementedException();
    }
}