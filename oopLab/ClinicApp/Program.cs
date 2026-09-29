using Lab1;

Patient p1 = new Patient("Iван", "Петренко", new DateTime(1985, 8, 9), "A+", BloodType.APositive, "0501234567", "");
Patient p2 = new Patient("Олена", "Коваль", new DateTime(1993, 9, 10), "B-", BloodType.BNegative, "0672345678", "");
Patient p3 = new Patient("Максим", "Бойко", new DateTime(2010, 8, 9), "O+", BloodType.ONegative, "0933456789", "");
Patient p4 = new Patient("Олена", "Коваль", new DateTime(2010, 9, 10), " " ,"0000000000");
Patient p5 = new Patient("Марія", "Ткач", new DateTime(2000, 7, 17), " ", "0000000000");

Console.WriteLine("\tСписок пацієнтів \n");
Console.Write(p1);
Console.Write(p2);
Console.Write(p3);
Console.Write(p4);
Console.Write(p5);

Doctor d1 = new Doctor("Олег", "Сидоренко", Speciality.Cardiology, "LIC-001", "0441234567",  8, 16);
Doctor d2 = new Doctor("Наталія", "Мороз", Speciality.Neurology, "LIC-002", "0442345678",  9, 18);
Doctor d3 = new Doctor("Андрій", "Власенко", Speciality.Pediatrics, "LIC-003", "0443456789", 8, 17);

Console.WriteLine("\tСписок лікарів");
Console.WriteLine(d1);
Console.WriteLine(d2);
Console.WriteLine(d3);

Console.WriteLine("\t Записи \t");

PatientManager pm = new PatientManager();
pm.Add(p1);
pm.Add(p2);
pm.Add(p3);
pm.Add(p4);
pm.Add(p5);

Console.WriteLine("\n\tВивід усіх пацієнтів:");
pm.DisplayAll();

Console.WriteLine("\n\tСтатистика:");
pm.DisplayStats();

DoctorManager doctorManager = new DoctorManager();

Console.WriteLine("\n\t Додавання ");
doctorManager.Add(d1);
doctorManager.Add(d2);
doctorManager.Add(d3);

Console.WriteLine("\n=== Список ");
doctorManager.DisplayAll();

Console.WriteLine("\n\t Статистика ");
doctorManager.DisplayStats();

Console.WriteLine("\n\tПошук та видалення ");

string spec = "Кардіологія";
Console.WriteLine($"\n\tПошук за спеціальністю \"{spec}\" ");
Doctor[] cardiologists = doctorManager.FindBySpeciality(spec);
foreach (var doc in cardiologists)
{
    Console.WriteLine($"Знайдено: [{doc.Id}] {doc.FullName} ({doc.Specialty})");
}

Console.WriteLine($"\n\tПошук лікаря ID ({d1.Id}) ");
Doctor? foundDoctor = doctorManager.FindById(d1.Id);
if (foundDoctor != null)
{
    Console.WriteLine($"Знайдено: {foundDoctor.FullName}, Ліцензія: {foundDoctor.LicenseNumber}");
}

Console.WriteLine($"\n\tВидалення лікаря з ID ({d2.Id})");
bool isDoctorRemoved = doctorManager.Remove(d2.Id);
Console.WriteLine(isDoctorRemoved ? "Лікаря успішно видалено!" : "Лікаря не знайдено.");

Console.WriteLine("\n\tОновленний список");
doctorManager.DisplayAll();

Appointment a1 = new Appointment(p1.Id, d1.Id, DateTime.Now.AddDays(1).AddHours(2), 30);
Appointment a2 = new Appointment(p2.Id, d2.Id, DateTime.Now.AddDays(2).AddHours(1), 45);
Appointment a3 = new Appointment(p1.Id, d2.Id, DateTime.Now.AddHours(-5), 20); 

Console.WriteLine("Початкові записи:");
Console.WriteLine(a1);
Console.WriteLine(a2);
Console.WriteLine(a3);
Console.WriteLine("\n\t Зміна статусів ");

a3.Complete();
Console.WriteLine($"Запис #{a3.Id} завершено: {a3}");

a2.Cancel("Пацієнт захворів");
Console.WriteLine($"Запис #{a2.Id} скасовано: {a2}");

AppointmentManager appointmentManager = new AppointmentManager(pm, doctorManager);

appointmentManager.Book(p1.Id, d1.Id, new DateTime(2026, 5, 9, 10, 0, 0), 30);
appointmentManager.Book(p2.Id, d2.Id, new DateTime(2026, 5, 9, 11, 0, 0), 45);
appointmentManager.Book(p3.Id, d3.Id, new DateTime(2026, 5, 10, 9, 0, 0), 20);
Console.WriteLine();

Console.WriteLine("Майбутні записи:");
appointmentManager.DisplayList(appointmentManager.GetUpcoming());

Console.WriteLine();

appointmentManager.Cancel(1);

Console.WriteLine();

Console.WriteLine($"Записи пацієнта #{p2.Id}:");
appointmentManager.DisplayList(appointmentManager.GetByPatient(p2.Id));

Clinic clinic = new Clinic("Медична Клініка");


clinic.Patients.Add(p1);
clinic.Patients.Add(p2);

clinic.Doctors.Add(d1);
clinic.Doctors.Add(d2);

clinic.Appointments.Book(p1.Id, d1.Id, DateTime.Now.AddDays(1), 30);
clinic.Appointments.Book(p2.Id, d2.Id, DateTime.Now.AddDays(2), 45);

Console.WriteLine();
clinic.DisplaySchedule(DateTime.Now.AddDays(1));

Console.WriteLine();
clinic.GenerateReport();