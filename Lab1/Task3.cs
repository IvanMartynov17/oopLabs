namespace Task1;

public static class Task3
{
    public static void Run()
    {
        int birthYear = 0, age = 0;
        const int currentYear = 2026;
        Console.Write("Enter your birth year: ");
        birthYear = int.Parse(Console.ReadLine());
        
        age = currentYear - birthYear;
        Console.Write($"Вік: {age} ");

        if (age < 18)
        {
            Console.WriteLine("Дитина");
        }else if (age >= 18 && age < 60)
        {
            Console.WriteLine("Дорослий");
        }
        else
        {
            Console.WriteLine("Пенсіонер");
        }
    }
}