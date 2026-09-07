namespace Task1;

public class Task3
{
    public static void Run()
    {
        int birthYear = 0, currentYear = 0, age = 0;
        Console.Write("Enter your birth year: ");
        birthYear = int.Parse(Console.ReadLine());
        
        currentYear = DateTime.Now.Year;
        
        age = currentYear - birthYear;
        Console.Write($"Age: {age} ");

        if (age < 18)
        {
            Console.WriteLine("Child");
        }else if (age >= 18 && age < 60)
        {
            Console.WriteLine("Adult");
        }
        else
        {
            Console.WriteLine("Pensioner");
        }
    }
}