using Lab1;

Patient p1 = new Patient("Iван", "Петренко", new DateTime(1985, 8, 9), "A+", "A+", "0501234567", "");
Patient p2 = new Patient("Олена", "Коваль", new DateTime(1993, 9, 10), "B-", "B-", "0672345678", "");
Patient p3 = new Patient("Максим", "Бойко", new DateTime(2010, 8, 9), "O+", "O+", "0933456789", "");
Patient p4 = new Patient("Олена", "Коваль", new DateTime(2010, 9, 10), " " ,"0000000000");
Patient p5 = new Patient("Марія", "Ткач", new DateTime(2000, 7, 17), " ", "0000000000");

Console.WriteLine("\tСписок пацієнтів \n");
Console.Write(p1);
Console.Write(p2);
Console.Write(p3);
Console.Write(p4);
Console.Write(p5);

Doctor d1 = new Doctor("Олег", "Сидоренко", "Кардіологія", "LIC-001", "0441234567",  8, 16);
Doctor d2 = new Doctor("Наталія", "Мороз", "Неврологія", "LIC-002", "0442345678",  9, 18);
Doctor d3 = new Doctor("Андрій", "Власенко", "Педіатрія", "LIC-003", "0443456789", 8, 17);

Console.WriteLine("\tСписок лікарів");
Console.WriteLine(d1);
Console.WriteLine(d2);
Console.WriteLine(d3);