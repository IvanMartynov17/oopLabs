namespace Task1;

public static class Task7
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
        decimal maxNumber = price[0];
        decimal minNumber = price[0];
        
        foreach (decimal sessionPrice in price)
        {
         sum += sessionPrice;
         if (sessionPrice < minNumber)
         {
             minNumber = sessionPrice;
         }
         if (sessionPrice > maxNumber)
             {
             maxNumber = sessionPrice;
             }
        }
        decimal average = sum / N;
        
        Console.WriteLine("Minimum number is {0}", minNumber);
        Console.WriteLine("Maximum number is {0}", maxNumber);
        Console.WriteLine("Average number is {0}", average);

        int idx = 0, firstIndexGreater1000 = -1;
        while (idx < N)
        {
            if (price[idx] > 1000)
            {
                firstIndexGreater1000 = idx;
                break;
            }
            idx++;
        }
        int aboveAverageCount = 0;
        for (int i = 0; i < N; i++)
        {
            if (price[i] > average)
            {
                aboveAverageCount++;
            }
        }
        if (firstIndexGreater1000 != -1)
        {
            int ordinalNumber = firstIndexGreater1000 + 1;
            decimal firstValue = price[firstIndexGreater1000];
            Console.WriteLine($"Перший > 1000: #({ordinalNumber} - {firstValue:F2 )} грн");
        }
        else
        {
            Console.WriteLine($"Перший > 1000: немає");
        }
        Console.WriteLine("========================");
    }
    

    
}