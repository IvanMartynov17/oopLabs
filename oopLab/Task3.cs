namespace Lab1;

public static class Task3
{
    public static void Run()
    {
        string[] days = {"Понеділок", "Вівторок", "Середа", 
            "Четвер", "П'ятниця", "Субота", "Неділя"};
        int[] counts = new int[7];

        for (int i = 0; i < 7; i++)
        {
            Console.Write($"{days[i],-10} : ");
            counts[i] = int.Parse(Console.ReadLine());
        }

        int total = 0;
        int maxIdx = 0;
        int minIdx = 0;
        
        for (int i = 0; i < 7; i++)
        {
            total += counts[i];

            if (counts[i] > counts[maxIdx])
            {
                maxIdx = i;
            }

            if (counts[i] < counts[minIdx])
            {
                minIdx = i;
            }
        }

        Console.WriteLine("\tРезультат: ");
        for (int i = 0; i < 7; i++)
        {
            Console.Write($"{days[i],-10} : ");
            Console.Write($"{counts[i]} пацієнтів\n");
        }

        Console.WriteLine($"{"Разом: ", -10} {total}");
        Console.WriteLine($"{"Найменше: ", -10} {days[minIdx]} ({counts[minIdx]})");
        Console.WriteLine($"{"Найбільше: ", -10} {days[maxIdx]} ({counts[maxIdx]})");
        
        
    }
}