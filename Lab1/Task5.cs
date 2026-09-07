namespace Task1;

public class Task5
{
    public static void Run()
    {
        int day;
        
        Console.Write("Enter the day of the week: ");
        day = int.Parse(Console.ReadLine());

        string info = day switch
        {
            1 => "Monday, 08:00 - 18:00",
            2 => "Tuesday, 09:00 - 18:00",
            3 => "Wednesday, 09:00 - 17:00",
            4 => "Thursday, 09:00 - 17:00",
            5 => "Friday, 08:00 - 16:00",
            6 => "Saturday, 09:00 - 14:00",
            7 => "Sunday, Weekday",
            _ => "Invalid day"
        };
        Console.WriteLine(info);
    }
}