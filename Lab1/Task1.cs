namespace Task1;

public class Task1
{
    public static void Run()
    {
        double weight;
        double height;

        Console.WriteLine("Enter your weight in kilograms: ");
        weight = double.Parse(Console.ReadLine());
        
        Console.WriteLine("Enter your height in cm: ");
        height = double.Parse(Console.ReadLine());

        double bmi = weight / (height * height) * 10000;
        Console.Write($"BMI: {bmi:F2}");
    }
}