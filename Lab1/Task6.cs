namespace Task1;

public static class Task6
{
    public static void Run()
    {
        int cardNumber = 0;
        Console.Write("Enter your card number: ");
        cardNumber = int.Parse(Console.ReadLine());
        
        int lastDigit = cardNumber % 10;

        string last = lastDigit switch
        {
            0 or 1 => "Відділення 'загальна терапія'",
            2 or 3 => "Відділення 'хірургія'",
            4 or 5 => "Відділення 'кардіологія'",
            6 or 7 => "Відділення 'неврологія'",
            8 or 9 => "Відділення 'офтальмологія'",
            _ => "Invalid number"
        };
        Console.WriteLine(last);
        if (cardNumber % 2 == 0)
        {
            Console.WriteLine("Пільгова картка: так");
        }
        else
        {
            Console.WriteLine("Пільгова картка: ні"); 
        }
        
        if (cardNumber % 3 == 0)
        {
            Console.WriteLine("Черговий огляк: так");
        }
        else
        {
            Console.WriteLine("Черговий огляк: ні");
        }
    }
}