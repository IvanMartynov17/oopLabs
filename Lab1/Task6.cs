namespace Task1;

public class Task6
{
    public static void Run()
    {
        int cardNumber = 0;
        Console.Write("Enter your card number: ");
        cardNumber = int.Parse(Console.ReadLine());
        
        int lastDigit = cardNumber % 10;

        string last = lastDigit switch
        {
            0 or 1 => "Therapy",
            2 or 3 => "Surgeon",
            4 or 5 => "Cardio",
            6 or 7 => "Neurosurgeon",
            8 or 9 => "Opthalmologist",
            _ => "Invalid number"
        };
        Console.WriteLine(last);
        if (cardNumber % 2 == 0)
        {
            Console.WriteLine("PilgCard");
        }else if (cardNumber % 3 == 0)
        {
            Console.WriteLine("QueueView");
        }
    }
}