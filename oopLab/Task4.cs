namespace Lab1;

public static class Task4
{
    public static void Run()
    {
        int docNumber = 0;
        Console.Write("Enter number of doctors:");
        docNumber = int.Parse(Console.ReadLine());

        int daysCount = 0;
        Console.Write("Enter number of days:");
        daysCount = int.Parse(Console.ReadLine());
        
        int[,] receptions = new int[docNumber, daysCount];
        for (int i = 0; i < docNumber; i++)
        {
            string[] rowValues = Console.ReadLine().Split(' ');
            for (int j = 0; j < daysCount; j++)
            {
                receptions[i, j] = int.Parse(rowValues[j]);
            }
        }

        for (int i = 0; i < docNumber; i++)
        {
            int rowSum = 0;
            for (int j = 0; j < daysCount; j++)
            {
                rowSum += receptions[i, j];
            }
            Console.WriteLine($"Лікар {i + 1}: {rowSum} прийомів");
        }
        int[] colSum = new int [daysCount];
        for (int j = 0; j < daysCount; j++)
        {
            
            for (int i = 0; i < docNumber; i++)
            {
                colSum[j] += receptions[i, j];
            }
        }

        Console.Write($"По днях: ");
        for (int j = 0; j < daysCount; j++)
        {
            Console.Write($"{colSum[j]}, ");
        }
        int maxRow = 0;
        int maxCol = 0;
        for (int i = 0; i < docNumber; i++)
        {
            for (int j = 0; j < daysCount; j++)
            {
                if (receptions[i, j] > receptions[maxRow, maxCol])
                {
                    maxRow = i;
                    maxCol = j;
                }
            }
        }
        Console.WriteLine();
        Console.Write($"Максимум: {receptions[maxRow, maxCol]}, (Лікар {maxRow + 1}, День {maxCol + 1})");
    }
}