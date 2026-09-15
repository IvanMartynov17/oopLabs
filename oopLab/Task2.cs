namespace Lab1;

public static class Task2
{
    public static void Run()
    {
        int n = 0;
        Console.WriteLine("Enter a number: ");
        n = int.Parse(Console.ReadLine());

        int[] price = new int[n];
        Console.WriteLine($"Enter numbers:");
        for (int i = 0; i < n; i++)
        {
            Console.Write($"Enter price of product {i + 1}: ");
            price[i] = int.Parse(Console.ReadLine());
        }

        for (int i = 0; i < price.Length - 1; i++)
        {
            for (int j = 0; j < price.Length - 1 - i; j++)
            {
                if (price[j] > price[j + 1])
                {
                    int temp = price[j];
                    price[j] = price[j + 1];
                    price[j + 1] = temp;
                }
            }
        }

        Console.WriteLine($"Price of product after sort: ");
        for (int i = 0; i < price.Length; i++)
        {
            Console.Write($"{price[i]} -- ");
        }
        Console.WriteLine("\n");
        int min = price[0];
        int max = price[0];

        for (int i = 0; i < price.Length; i++)
        {
            if (max < price[i])
                max = price[i];
            
            if (min > price[i])
                min = price[i];
        }
        Console.WriteLine($"Min price: {min}");
        Console.WriteLine($"Max price: {max}");
    }
}