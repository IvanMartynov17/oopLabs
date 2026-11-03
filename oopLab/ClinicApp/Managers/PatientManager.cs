using ClinicApp.Models;
using ClinicApp.Enums;
namespace ClinicApp.Managers;

public class PatientManager
{
    private const int MaxPatients = 100;
    private readonly List<Patient> _patients = new List<Patient>();
    public int Count => _patients.Count;


    public Patient this[int index]
    {
        get => _patients[index];
        set => _patients[index] = value;
    }

    public bool TryFindById(int id, out Patient? patient)
    {
        patient = FindById(id);
        return patient != null;
    }

    public Patient[] FindByBloodType(BloodType bloodType)
    {
        List<Patient> result = new List<Patient>();

        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].BloodType == bloodType)
            {
                result.Add(_patients[i]);
            }
        }

        return result.ToArray();
    }

    public PatientManager()
    {
    }

    public void Add(Patient patient)
    {
        _patients.Add(patient);
        Console.WriteLine($"Пацієнта: {patient.FirstName} {patient.LastName} додано.");
    }

    public Patient? FindById(int id)
    {
        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].Id == id)
            {
                return _patients[i];
            }
        }

        return null;
    }

    public Patient[] FindByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Array.Empty<Patient>();
        }

        string search = name.Trim().ToLower();
        List<Patient> result = new List<Patient>();

        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].FirstName.ToLower().Contains(search) ||
                _patients[i].LastName.ToLower().Contains(search))
            {
                result.Add(_patients[i]);
            }
        }

        return result.ToArray();
    }

    public bool Remove(int id)
    {
        int targetIndex = -1;
        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].Id == id)
            {
                targetIndex = i;
                break;
            }
        }

        if (targetIndex == -1)
        {
            return false;
        }

        _patients.RemoveAt(targetIndex);
        return true;
    }

    public Patient[] GetAll()
    {
        return _patients.ToArray();
    }
    public void DisplayAll()
    {
        Console.WriteLine($"--- Пацієнти ({_patients.Count}) ---");
        for (int i = 0; i < _patients.Count; i++)
        {
            Patient p = _patients[i];
            string status = p.IsAdult ? "Дорослий" : "Дитина";
            Console.WriteLine($"[{p.Id}] {p.FullName} | Вік: {p.Age} ({status}) | Кров: {p.BloodType} | Тел: {p.Phone}");
        }

        Console.WriteLine(new string('=', 35));
    }

    public void DisplayStats()
    {
        if (_patients.Count == 0)
        {
            Console.WriteLine("=== Статистика порожня ===");
            return;
        }

        int sumAge = 0;
        int minIndex = 0;
        int maxIndex = 0;
        int adultCount = 0;

        for (int i = 0; i < _patients.Count; i++)
        {
            int currentAge = _patients[i].Age;
            sumAge += currentAge;

            if (currentAge < _patients[minIndex].Age)
            {
                minIndex = i;
            }

            if (currentAge > _patients[maxIndex].Age)
            {
                maxIndex = i;
            }

            if (_patients[i].IsAdult)
            {
                adultCount++;
            }
        }

        double averageAge = (double)sumAge / _patients.Count;
        Patient youngest = _patients[minIndex];
        Patient oldest = _patients[maxIndex];

        Console.WriteLine("=== Статистика пацієнтів ===");
        Console.WriteLine($"Всього:          {_patients.Count}");
        Console.WriteLine($"Середній вік:    {averageAge:F1} р.");
        Console.WriteLine($"Наймолодший:     {youngest.FullName} ({youngest.Age} р.)");
        Console.WriteLine($"Найстарший:      {oldest.FullName} ({oldest.Age} р.)");
        Console.WriteLine($"Дорослих:        {adultCount} з {_patients.Count}");
        Console.WriteLine(new string('=', 35));
    }
}