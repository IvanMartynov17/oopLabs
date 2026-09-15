namespace Lab1;

public static class Task6
{
    public static void Run()
    {
        Console.Write("Enter numbers of doctors");
        int n = int.Parse(Console.ReadLine());
        
        int[][] costs = new int[n][];

        for (int i = 0; i < n; i++)
        {
            int appointments = int.Parse(Console.ReadLine());
            costs[i] = new int[appointments];

            for (int j = 0; j < appointments; j++)
            {
                costs[i][j] = int.Parse(Console.ReadLine());
            }
        }

        int maxIncome = 0;
        int maxIncomeDoctorIndex = 0;

        for (int i = 0; i < costs.Length; i++)
        {
            int totalIncome = 0;

            for (int j = 0; j < costs[i].Length; j++)
            {
                totalIncome += costs[i][j];
            }

            double average = costs[i].Length > 0 ? (double)totalIncome / costs[i].Length : 0;
            Console.Write($"Лікар {i + 1}: Прийоми, сума={costs[i].Length}, середня = {average} грн\n");


            if (totalIncome > maxIncome)
            {
                maxIncome = totalIncome;
                maxIncomeDoctorIndex = i + 1;
            }
        }

        Console.WriteLine($"Найбільший дохід: Лікар {maxIncomeDoctorIndex} ({maxIncome} грн)");
    }
}