namespace Lab1;

public static class Task8
{
    public static void Run()
    {
        int departments = 0;
        int weeks = 0;
        int alternations = 2;

        int maxPatients = 0;
        int maxDepartment = 0;
        
        Console.Write("Enter number of departments: ");
        departments = int.Parse(Console.ReadLine());

        Console.Write("Enter number of weeks: ");
        weeks = int.Parse(Console.ReadLine());

        int[,,] data = new int[departments, weeks, alternations];
        for (int department = 0; department < departments; department++)
        {
            for (int week = 0; week < weeks; week++)
            {
                for (int alternation = 0; alternation < alternations; alternation++)
                {
                    Console.Write($"Department: {department + 1}, " +
                                  $"Week: {week + 1}, " +
                                  $"Alternation: {alternation + 1}: ");
                    data[department, week, alternation] = int.Parse(Console.ReadLine());
                }
            }
        }

        for (int department = 0; department < departments; department++)
        {
            Console.WriteLine($"Відділення: {department + 1}");
            int totalDepartmentPatients = 0;
            for (int week = 0; week < weeks; week++)
            {
                
                int morning = data[department, week, 0];
                int afternoon = data[department, week, 1];
                int totalWeek = morning + afternoon;
                
                totalDepartmentPatients += totalWeek;
                
                Console.WriteLine($"Тиждень: {week + 1}, " +
                                  $"ранок: {morning}, вечір: {afternoon}, " +
                                  $" -> разом {totalWeek}" );
            }
            Console.WriteLine($"Разом: {totalDepartmentPatients} пацієнтів");
         
            if (totalDepartmentPatients > maxPatients)
            {
                maxPatients = totalDepartmentPatients;
                maxDepartment = department + 1;
            }
        }

        Console.Write($"Найзавантаженіше: Відділення {maxDepartment} ({maxPatients} пацієнтів)");
    }
}