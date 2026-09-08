namespace Task1;

public static class Task5
{
    public static void Run()
    {
        int day;
        
        Console.Write("Enter the day of the week: ");
        day = int.Parse(Console.ReadLine());

        string info = day switch
        {
            1 => "День: Понеділок, 08:00 - 18:00",
            2 => "День: Вівторок, 08:00 - 18:00",
            3 => "День: Середа, 08:00 - 17:00",
            4 => "День: Четвер, 08:00 - 18:00",
            5 => "День: П'ятниця, 08:00 - 16:00",
            6 => "День: Субота, 09:00 - 14:00",
            7 => "День: Неділя - вихідний",
            _ => "невідомий день"
        };
        Console.WriteLine(info);
    }
}