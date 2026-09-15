namespace Lab1;

public static class Task5
{
    public static void Run()
    {
        int size = 0;
        Console.WriteLine("Enter size of array: ");
        size = int.Parse(Console.ReadLine());
        
        int[,] matrix = new int[size, size];
        Console.WriteLine("Enter matrix: ");
        for (int i = 0; i < size; i++)
        {
            string[] rowValues = Console.ReadLine().Split(' ');
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = int.Parse(rowValues[j]);
            }
        }
        int[] mainDiagonal = new int[size];
        int mainDiagonalSum = 0;
        for (int i = 0; i < size; i++)
        {
            
            for (int j = 0; j < size; j++)
            {
                if (i == j)
                {
                    mainDiagonal[i] = matrix[i, j];
                    mainDiagonalSum += mainDiagonal[i];
                }
            }
        }
        Console.Write($"Головна діагональ: ");
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{mainDiagonal[i]}, ");
        }
        Console.WriteLine($"(сума = {mainDiagonalSum})");
        int[] secondaryDiagonal = new int[size];
        int secondaryDiagonalSum = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i + j == size - 1)
                {
                    secondaryDiagonal[i] = matrix[i, j];
                    secondaryDiagonalSum += secondaryDiagonal[i];
                }
            }
        }
        Console.Write($"Побічна діагональ: ");
        for (int i = 0; i < size; i++)
        {
            Console.Write($"{secondaryDiagonal[i]}, ");
        }
        Console.WriteLine($"(сума = {secondaryDiagonalSum})");
    }
}