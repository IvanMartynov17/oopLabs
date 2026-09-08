namespace Task1;

public static class Task4
{
    public static void Run()
    {
        int systolic = 0, diastolic = 0;
        string category;
        Console.Write("Enter your systolic blood pressure: ");
        systolic = int.Parse(Console.ReadLine());
        
        Console.Write("Enter your diastolic blood pressure: ");
        diastolic = int.Parse(Console.ReadLine());

        if (systolic < 120 && diastolic < 80)
        {
            category = "Normal";
            Console.WriteLine($"Тиск:{systolic}/{diastolic} - {category}");
        }else if (systolic < 130 && diastolic < 80)
        {
            category = "Increased";
            Console.WriteLine($"Тиск:{systolic}/{diastolic} - {category}");
        }else if (systolic < 140 && diastolic < 90)
        {
            category = "1Stage hypertension";
            Console.WriteLine($"Тиск:{systolic}/{diastolic} - {category}");
        }
        else
        {
            category = "2Stage hypertension";
            Console.WriteLine($"Тиск:{systolic}/{diastolic} - {category}");
        }
    }
}