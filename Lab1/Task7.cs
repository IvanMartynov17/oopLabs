namespace Task1;

public class Task7
{
    public static void Run()
    {
        int N = 0;
        Console.WriteLine("Enter a number between 1 and 100");
        N = int.Parse(Console.ReadLine());
        
        if (N < 0)
        {
            Console.WriteLine("Invalid number");
        }
        
        decimal[] price = new decimal[N];
        for (int i = 0; i < N; i++)
        {
            Console.Write($"Enter prices {i + 1}: ");
           price[i] = decimal.Parse(Console.ReadLine());
        }

        decimal sum = 0;
        foreach (decimal sessionPrices in price)
        {
         sum += sessionPrices;
        }
        Console.WriteLine($"The total price of {N} is {sum}");
        
        decimal minNumber = price[0];
        foreach (decimal sessionPrices in price)
        {
            if (sessionPrices < minNumber)
            {
                minNumber = sessionPrices;
                
            }
        }
        Console.WriteLine("Minimum number is {0}", minNumber);
        
        decimal maxNumber = price[0];
        foreach (decimal sessionPrices in price)
        {
            if (sessionPrices > maxNumber)
                {
                maxNumber = sessionPrices;
                }
        }
        Console.WriteLine("Maximum number is {0}", maxNumber);

        decimal average = sum / N;
        Console.WriteLine("Average number is {0}", average);

        int n = 0, count = 0;
        while (n < N)
        {
            if (price[n] > 1000)
            {
                count++;
            }
            n++;
        }
        Console.WriteLine($"Prices more than 1000 is {count}");
        if (count == 0)
        {
            Console.WriteLine("None");
        }

        n = 0;
        int moreThanAverage = 0;
        if (price[n] > average)
        {
            moreThanAverage++;
        }

        if (moreThanAverage == 0)
        {
            Console.WriteLine("No more than average");
        }
        else
        {
            Console.WriteLine($"Numbers more than average: {moreThanAverage}");
        }
    }
    

    
}