namespace Task1;

public class Task8
{
    public static double CalculateBMI(double weight, double height)
    {
        return weight / (height * height) * 10000;
    }

    public static string GetBMICategory(double bmi)
    {
        if (bmi > 18.5 && bmi < 24.9)
        {
            return "Norma";
        }
        else if (bmi > 25 && bmi < 29.9)
        {
            return "Excess weight";
        }
        else if (bmi > 30)
        {
            return "Overweight";
        }else
        {
            return "Underweight";
        }
    }

    public static double CalculateCost(double price, int discount, int count)
    {
        return price * count * (1 - discount / 100.0);
    }

    public static string GetAgeCategory(int birthYear)
    {
        int currentYear = DateTime.Now.Year;
        int age = currentYear - birthYear;
        if (age < 18)
        {
            return "Child";
        }
        else if (age >= 18 && age < 60)
        {
            return "Adult";
        }
        else
        {
            return "Pensioner";
        }
    }

    public static string GetPressureStatus(int systolic, int diastolic)
    {   
        if (systolic < 120 && diastolic < 80)
        {
            return "Normal";
        }else if (systolic < 130 && diastolic < 80)
        {
        return "Increased";
        }else if (systolic < 140 && diastolic < 90)
        {
        return "1Stage hypertension";
        }
        else
        {
        return "2Stage hypertension";
        } 
    }
    public static void Run()
    {
        int weight = 0, height = 0;
        Console.Write("Enter your weight ");
        weight = int.Parse(Console.ReadLine());
        Console.Write("Enter your height ");
        height = int.Parse(Console.ReadLine());
        CalculateBMI(weight, height);
        Console.WriteLine("BMI: " + GetBMICategory(CalculateBMI(weight, height)));

        double bmi = CalculateBMI(weight, height);
        GetBMICategory(bmi);
        Console.WriteLine("BMI: " + GetBMICategory(bmi));
        
        double price = 0;
        int discount, count;
        Console.Write("Enter the price of the product");
        price = double.Parse(Console.ReadLine());
        Console.Write("Enter the number of products");
        count = int.Parse(Console.ReadLine());
        Console.Write("Enter the discount");
        discount = int.Parse(Console.ReadLine());
        CalculateCost(price, discount, count);
        Console.WriteLine("Total cost: " + CalculateCost(price, discount, count));

        int birthYear = 0;
        int currentYear = DateTime.Now.Year;
        Console.Write("Enter your birth year");
        birthYear = int.Parse(Console.ReadLine());
        GetAgeCategory(birthYear);
        int age = currentYear - birthYear;
        Console.WriteLine("Age category: " + GetAgeCategory(birthYear));
        Console.WriteLine("Age: " + age);
        
        int systolic = 0, diastolic = 0;
        Console.Write("Enter your systolic blood pressure");
        systolic = int.Parse(Console.ReadLine());
        Console.Write("Enter your diastolic blood pressure");
        diastolic = int.Parse(Console.ReadLine());
        GetPressureStatus(systolic, diastolic);
        Console.WriteLine("Pressure status: " + GetPressureStatus(systolic, diastolic));
    }
}