namespace Lab1;

public static class Task7
{
    public static void Run()
    {
        int n;
        Console.Write("Enter the number of names: ");
        n = int.Parse(Console.ReadLine());
        
        Console.WriteLine("Enter names: ");
        string[] names = new string[n];
        for (int i = 0; i < n; i++)
        {
            names[i] = Console.ReadLine();
        }
        double[] IMT = new double[n];
        
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Enter IMT of {names[i]}: ");
            IMT[i] = double.Parse(Console.ReadLine());
        }

        for (int i = 0; i < n; i++)
        {
            var user = (names[i], IMT[i]);
            Console.WriteLine($"{user}");
        }

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (IMT[j] < IMT[j + 1])
                {
                    var temp = IMT[j];
                    IMT[j] = IMT[j + 1];
                    IMT[j + 1] = temp;
                }
            }
        }

        Console.WriteLine($"=== Рейтинг ІМТ ===");
        for (int i = 0; i < n; i++)
        {
            var user = (names[i], IMT[i]);
            Console.WriteLine($"#{i+1} {user}");
        }
    }
}