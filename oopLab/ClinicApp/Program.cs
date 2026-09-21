using Lab1;

Patient p1 = new Patient("Iван", "Петренко", new DateTime(1985, 8, 9), "A+", "A+", "0501234567", "");
Patient p2 = new Patient("Олена", "Коваль", new DateTime(1993, 9, 10), "B-", "B-", "0672345678", "");
Patient p3 = new Patient("Максим", "Бойко", new DateTime(2010, 8, 9), "O+", "O+", "0933456789", "");
Patient p4 = new Patient("Олена", "Коваль", new DateTime(2010, 9, 10), " " ,"0000000000");
Patient p5 = new Patient("Марія", "Ткач", new DateTime(2000, 7, 17), " ", "0000000000");


Console.Write(p1);
Console.Write(p2);
Console.Write(p3);
Console.Write(p4);
Console.Write(p5);
