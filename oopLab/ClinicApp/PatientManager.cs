namespace Lab1;

public class PatientManager
{
    private const int MaxPatients = 100;
    private Patient[] _patients = new Patient[MaxPatients];
    private int _count;
    public int Count => _count;

    public void Add(Patient patient)
    {
        if (_count > MaxPatients)
        {
            Console.Write($"Error...");
            return;
        }
        else
        {
            _patients[_count] = patient;
            _count++;
            Console.WriteLine($"Пацієнта: {patient.FirstName} {patient.LastName} додано.");
        }
    }
    public Patient? FindById(int id)
    {
        foreach (var patient in _patients)
        {
            if (patient.Id == id)
            {
                return patient;
            }
        }
        return null;
    }

    public Patient[] FindByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return new Patient[0];
        }

        string search = name.Trim().ToLower();

        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].FirstName.ToLower().Contains(search) ||
                _patients[i].LastName.ToLower().Contains(search))
            {
                matches++;
            }
        }

        Patient[] result = new Patient[matches];
        
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].FirstName.ToLower().Contains(search) ||
                _patients[i].LastName.ToLower().Contains(search))
            {
                result[index++] = _patients[i];
            }
        }
        return result;
    }

    public bool Remove(int id)
    {
        int targetIndex = -1;
        for (int i = 0; i < _count; i++)
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

        for (int i = targetIndex; i < _count - 1; i++)
        {
            _patients[i] = _patients[i + 1];
        }
        
        _patients[_count - 1] = null!;
        _count--;
        return true;
    }

    public void DisplayAll()
    {
        Console.Write($"[{_count}] / {MaxPatients} ===");
        for (int i = 0; i < _count; i++)
        {
            Patient p = _patients[i];
            string status = p.IsAdult ? "Дорослий" : "Дитина";
            Console.WriteLine($"[{p.Id}] {p.FullName} | Вік: {p.Age} ({status}) | Кров: {p.BloodType} | Тел: {p.Phone}");
        }

        Console.WriteLine(new string('=', 35));
    }

    public void DisplayStats()
    {
        int sumAge = 0;
        int minIndex = 0;
        int maxIndex = 0;
        int adultCount = 0;

        for (int i = 0; i < _count; i++)
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
        double averageAge = (double)sumAge / _count;
        Patient youngest = _patients[minIndex];
        Patient oldest = _patients[maxIndex];

        Console.WriteLine("=== Статистика пацієнтів ===");
        Console.WriteLine($"Всього:          {_count}");
        Console.WriteLine($"Середній вік:    {averageAge:F1} р.");
        Console.WriteLine($"Наймолодший:     {youngest.FullName} ({youngest.Age} р.)");
        Console.WriteLine($"Найстарший:      {oldest.FullName} ({oldest.Age} р.)");
        Console.WriteLine($"Дорослих:        {adultCount} з {_count}");
        Console.WriteLine(new string('=', 35));
    }
    
}