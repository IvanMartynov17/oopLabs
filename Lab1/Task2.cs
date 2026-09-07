using System.Runtime.Intrinsics.X86;

namespace Task1;

public class Task2
{
    public static void Run()
    {
        double price = 0, result;
        int count = 0, discount = 0;
        
        Console.Write("Enter the price of the product: ");
        price = double.Parse(Console.ReadLine());
        
        Console.Write("Enter the number of products: ");
        count = int.Parse(Console.ReadLine());
        
        Console.Write("Enter the discount: ");
        discount = int.Parse(Console.ReadLine());
        
        result = price * count * (1 - discount / 100.0);
        Console.Write($"Result: {result:F2}");
    }
}