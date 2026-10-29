using System;
using System.Globalization;
using System.Threading;
using ClinicApp;
using ClinicApp.Models;
using ClinicApp.Enums;
using ClinicApp.Managers;
using ClinicApp.Utils;
using ClinicApp.Interfaces;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

Clinic clinic = new Clinic("Медична Клініка");

Patient p1 = new Patient("Іван", "Петренко", new DateTime(1985, 8, 9), "A+", BloodType.APositive, "0501234567", "");
Patient p2 = new Patient("Олена", "Коваль", new DateTime(1993, 9, 10), "B-", BloodType.BNegative, "0672345678", "");
Patient p3 = new Patient("Максим", "Бойко", new DateTime(2010, 8, 9), "O+", BloodType.ONegative, "0933456789", "");

clinic.Patients.Add(p1);
clinic.Patients.Add(p2);
clinic.Patients.Add(p3);

Doctor d1 = new Doctor("Олег", "Сидоренко", Speciality.Cardiology, "LIC-001", "0441234567", 8, 16);
Doctor d2 = new Doctor("Наталія", "Мороз", Speciality.Neurology, "LIC-002", "0442345678", 9, 18);
Doctor d3 = new Doctor("Андрій", "Власенко", Speciality.Pediatrics, "LIC-003", "0443456789", 8, 17);

clinic.Doctors.Add(d1);
clinic.Doctors.Add(d2);
clinic.Doctors.Add(d3);

clinic.MedicalRecords.Add(new Diagnosis(p1.Id, d1.Id, DateTime.Today.AddDays(-30), "I10", "Гіпертонічна хвороба", isChronic: true));
clinic.MedicalRecords.Add(new Diagnosis(p1.Id, d2.Id, DateTime.Today.AddDays(-5), "J06.9", "Гострий ринофарингіт", isChronic: false));
clinic.MedicalRecords.Add(new LabResult(p1.Id, d1.Id, DateTime.Today.AddDays(-3), "Гемоглобін", 145, "г/л", "120–160", isNormal: true));
clinic.MedicalRecords.Add(new LabResult(p1.Id, d1.Id, DateTime.Today.AddDays(-2), "Холестерин", 6.2, "ммоль/л", "< 5.2", isNormal: false));
clinic.MedicalRecords.Add(new Prescription(p1.Id, d1.Id, DateTime.Today.AddDays(-5), "Лізиноприл", "10 мг", 30, "1 раз на добу вранці"));

clinic.MedicalRecords.Add(new Prescription(p2.Id, d2.Id, DateTime.Today.AddDays(-40), "Амоксицилін", "500 мг", 10, "3 рази на день"));
clinic.MedicalRecords.Add(new LabResult(p2.Id, d2.Id, DateTime.Today.AddDays(-10), "Глюкоза", 5.0, "ммоль/л", "3.3–5.5", isNormal: true));

clinic.MedicalRecords.Add(new Diagnosis(p3.Id, d3.Id, DateTime.Today.AddDays(-1), "J00", "Гострий назофарингіт", isChronic: false));

bool exit = false;
while (!exit)
{
    Console.WriteLine("================================================");
    Console.WriteLine("||           МЕДИЧНА КЛІНІКА                    ||");
    Console.WriteLine("==================================================");
    Console.WriteLine("||  1. Пацієнти    -    реєстрація, пошук       ||");
    Console.WriteLine("||  2. Лікарі      -    персонал, розклад       ||");
    Console.WriteLine("||  3. Записи      -    прийоми, скасування     ||");
    Console.WriteLine("||  4. Медична картка - діагнози, рецепти       ||");
    Console.WriteLine("||  5. Рахунки     -    оплата, борги           ||");
    Console.WriteLine("||  6. Звіт        -    загальна статистика     ||");
    Console.WriteLine("||  0. Вийти                                    ||");
    Console.WriteLine("==================================================");
    Console.Write("Оберіть команду: ");

    string? choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            PatientsMenu(clinic);
            break;
        case "2":
            DoctorsMenu(clinic);
            break;
        case "3":
            AppointmentsMenu(clinic);
            break;
        case "4":
            MedicalRecordsMenu(clinic);
            break;
        case "5":
            BillingMenu(clinic);
            break;
        case "6":
            clinic.GenerateReport();
            break;
        case "0":
            exit = true;
            Console.WriteLine("Завершення роботи");
            break;
        default:
            Console.WriteLine("Команди не існує.");
            break;
    }
}

static void PatientsMenu(Clinic clinic) => clinic.Patients.DisplayAll();

static void DoctorsMenu(Clinic clinic)
{
    bool back = false;
    while (!back)
    {
        Console.WriteLine("\n-- Лікарі --------------------");
        Console.WriteLine("1 - Список лікарів");
        Console.WriteLine("5 - Вільні години лікаря");
        Console.WriteLine("0 - Назад");
        Console.Write("Оберіть: ");

        string? choice = Console.ReadLine();
        Console.WriteLine();

        switch (choice)
        {
            case "1":
                clinic.Doctors.DisplayAll();
                break;
            case "5":
                ShowDoctorAvailableSlotsUI(clinic);
                break;
            case "0":
                back = true;
                break;
            default:
                Console.WriteLine("Некоректний пункт підменю.");
                break;
        }
    }
}

static void AppointmentsMenu(Clinic clinic)
{
    bool back = false;
    while (!back)
    {
        Console.WriteLine("\n-- Записи ----------------------");
        Console.WriteLine("1 - Список майбутніх записів");
        Console.WriteLine("8 - Скасувати всі записи пацієнта");
        Console.WriteLine("0 - Назад");
        Console.Write("Оберіть: ");

        string? choice = Console.ReadLine();
        Console.WriteLine();

        switch (choice)
        {
            case "1":
                clinic.Appointments.DisplayList(clinic.Appointments.GetUpcoming());
                break;
            case "8":
                CancelAllPatientAppointmentsUI(clinic);
                break;
            case "0":
                back = true;
                break;
            default:
                Console.WriteLine("Некоректний пункт підменю.");
                break;
        }
    }
}

static void BillingMenu(Clinic clinic)
{
    bool back = false;
    while (!back)
    {
        Console.WriteLine("\n-- Рахунки ----------------------");
        Console.WriteLine("1. Борги пацієнта");
        Console.WriteLine("2. Всі неоплачені записи");
        Console.WriteLine("3. Оплатити запис");
        Console.WriteLine("4. Загальний борг клініки");
        Console.WriteLine("0. Назад");
        Console.Write("Оберіть: ");

        string? choice = Console.ReadLine();
        Console.WriteLine();

        switch (choice)
        {
            case "1":
                Console.Write("ID пацієнта: ");
                if (!int.TryParse(Console.ReadLine(), out int patientId))
                {
                    break;
                }

                IPayable[] patientUnpaid = clinic.Billing.GetUnpaidByPatient(patientId);
                if (patientUnpaid.Length > 0)
                {
                    Console.WriteLine($"Неоплачені записи пацієнта #{patientId}:");
                    clinic.Billing.DisplayUnpaid(patientUnpaid);
                }
                else
                {
                    Console.WriteLine("Немає неоплачених записів.");
                }

                decimal debt = clinic.Billing.GetPatientDebt(patientId);
                Console.WriteLine($"Борг: {debt:F2} грн");
                break;

            case "2":
                IPayable[] allUnpaid = clinic.Billing.GetAllUnpaid();
                clinic.Billing.DisplayUnpaid(allUnpaid);
                break;

            case "3":
                Console.Write("ID запису для оплати: ");
                if (!int.TryParse(Console.ReadLine(), out int appointmentId))
                {
                    break;
                }

                if (clinic.Billing.PayAppointment(appointmentId))
                {
                    Console.WriteLine($"Запис [{appointmentId}] оплачено.");
                }
                else
                {
                    Console.WriteLine("Не вдалося оплатити: запис не знайдено, вже оплачено або скасовано.");
                }
                break;

            case "4":
                Console.WriteLine($"Загальний борг по клініці: {clinic.Billing.GetTotalDebt():F2} грн");
                break;

            case "0":
                back = true;
                break;

            default:
                Console.WriteLine("Некоректний пункт підменю.");
                break;
        }
    }
}

static void MedicalRecordsMenu(Clinic clinic)
{
    bool back = false;
    while (!back)
    {
        Console.WriteLine("\n------------ Медична картка ------------");
        Console.WriteLine("1 - Картка пацієнта (зведення)");
        Console.WriteLine("2 - Усі записи пацієнта");
        Console.WriteLine("3 - Додати діагноз");
        Console.WriteLine("4 - Додати аналіз");
        Console.WriteLine("5 - Додати рецепт");
        Console.WriteLine("6 - Записи лікаря");
        Console.WriteLine("0 - Назад");
        Console.Write("Оберіть: ");

        string? choice = Console.ReadLine();
        Console.WriteLine();

        switch (choice)
        {
            case "1":
                ShowPatientSummaryUI(clinic);
                break;
            case "2":
                ShowAllPatientRecordsUI(clinic);
                break;
            case "3":
                AddDiagnosisUI(clinic);
                break;
            case "4":
                AddLabResultUI(clinic);
                break;
            case "5":
                AddPrescriptionUI(clinic);
                break;
            case "6":
                ShowDoctorRecordsUI(clinic);
                break;
            case "0":
                back = true;
                break;
            default:
                Console.WriteLine("Некоректний пункт підменю.");
                break;
        }
    }
}

static void CancelAllPatientAppointmentsUI(Clinic clinic)
{
    Console.Write("ID пацієнта: ");
    if (!int.TryParse(Console.ReadLine(), out int patientId))
    {
        return;
    }

    Console.Write("Причина (Enter - без причини): ");
    string reason = Console.ReadLine() ?? "";

    Appointment[] patientAppointments = clinic.Appointments.GetByPatient(patientId);
    int cancelledCount = AppointmentManager.CancelAll(patientAppointments, reason);

    Console.WriteLine($"Скасовано записів: {cancelledCount}");
}

static void ShowDoctorAvailableSlotsUI(Clinic clinic)
{
    Console.Write("ID лікаря: ");
    if (!int.TryParse(Console.ReadLine(), out int doctorId))
    {
        return;
    }

    ISchedulable? schedulable = clinic.Doctors.FindById(doctorId);
    if (schedulable == null)
    {
        Console.WriteLine("Лікаря не знайдено.");
        return;
    }

    Console.Write("Дата (dd.MM.yyyy): ");
    string dateInput = Console.ReadLine() ?? "";
    if (!DateTime.TryParseExact(dateInput, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
    {
        return;
    }

    Console.Write("Скільки слотів: ");
    if (!int.TryParse(Console.ReadLine(), out int slotCount))
    {
        return;
    }

    try
    {
        DateTime[] slots = schedulable.GetAvailableSlots(date, slotCount);
        for (int i = 0; i < slots.Length; i++)
        {
            Console.WriteLine(slots[i].ToString("HH:mm"));
        }
    }
    catch (ArgumentOutOfRangeException ex)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
    }
}

static void ShowPatientSummaryUI(Clinic clinic)
{
    clinic.Patients.DisplayAll();
    Console.Write("ID пацієнта: ");
    if (!int.TryParse(Console.ReadLine(), out int patientId))
    {
        Console.WriteLine("Помилка: Некоректне число.");
        return;
    }

    Patient? patient = clinic.Patients.FindById(patientId);
    if (patient == null)
    {
        Console.WriteLine("Помилка: Пацієнта не знайдено.");
        return;
    }

    clinic.MedicalRecords.DisplayPatientSummary(patient.Id);
}

static void ShowAllPatientRecordsUI(Clinic clinic)
{
    clinic.Patients.DisplayAll();
    Console.Write("ID пацієнта: ");
    if (!int.TryParse(Console.ReadLine(), out int patientId))
    {
        Console.WriteLine("Помилка: Некоректне число.");
        return;
    }

    if (clinic.Patients.FindById(patientId) == null)
    {
        Console.WriteLine("Помилка: Пацієнта не знайдено.");
        return;
    }

    MedicalRecord[] records = clinic.MedicalRecords.GetByPatient(patientId);
    clinic.MedicalRecords.DisplayList(records);
}

static void AddDiagnosisUI(Clinic clinic)
{
    clinic.Patients.DisplayAll();
    clinic.Doctors.DisplayAll();

    Console.Write("ID пацієнта: ");
    if (!int.TryParse(Console.ReadLine(), out int patientId))
    {
        Console.WriteLine("Помилка: Некоректне число.");
        return;
    }

    Console.Write("ID лікаря: ");
    if (!int.TryParse(Console.ReadLine(), out int doctorId))
    {
        Console.WriteLine("Помилка: Некоректне число.");
        return;
    }

    if (clinic.Patients.FindById(patientId) == null || clinic.Doctors.FindById(doctorId) == null)
    {
        Console.WriteLine("Помилка: Пацієнта чи лікаря не знайдено.");
        return;
    }

    Console.Write("Код діагнозу (напр. J06.9): ");
    string code = Console.ReadLine() ?? "";

    Console.Write("Опис: ");
    string description = Console.ReadLine() ?? "";

    Console.Write("Хронічне? (1 - так, 0 - ні): ");
    string chronicInput = Console.ReadLine() ?? "";
    bool isChronic = chronicInput == "1";

    try
    {
        Diagnosis diagnosis = new Diagnosis(patientId, doctorId, DateTime.Today, code, description, isChronic);
        clinic.MedicalRecords.Add(diagnosis);
        Console.WriteLine($"Запис [{diagnosis.Id}] Діагноз додано.");
    }
    catch (ArgumentOutOfRangeException ex)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
    }
}

static void AddLabResultUI(Clinic clinic)
{
    clinic.Patients.DisplayAll();
    clinic.Doctors.DisplayAll();

    Console.Write("ID пацієнта: ");
    if (!int.TryParse(Console.ReadLine(), out int patientId))
    {
        Console.WriteLine("Помилка: Некоректне число.");
        return;
    }

    Console.Write("ID лікаря: ");
    if (!int.TryParse(Console.ReadLine(), out int doctorId))
    {
        Console.WriteLine("Помилка: Некоректне число.");
        return;
    }

    if (clinic.Patients.FindById(patientId) == null || clinic.Doctors.FindById(doctorId) == null)
    {
        Console.WriteLine("Помилка: Пацієнта чи лікаря не знайдено.");
        return;
    }

    Console.Write("Назва аналізу: ");
    string testName = Console.ReadLine() ?? "";

    Console.Write("Значення (число): ");
    if (!double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
    {
        Console.WriteLine("Помилка: Некоректне число.");
        return;
    }

    Console.Write("Одиниці виміру: ");
    string unit = Console.ReadLine() ?? "";

    Console.Write("Норма (напр. 4.0 - 9.0): ");
    string normalRange = Console.ReadLine() ?? "";

    Console.Write("В нормі? (1 - так, 0 - ні): ");
    string normalInput = Console.ReadLine() ?? "";
    bool isNormal = normalInput == "1";

    try
    {
        LabResult result = new LabResult(patientId, doctorId, DateTime.Today, testName, value, unit, normalRange, isNormal);
        clinic.MedicalRecords.Add(result);
        Console.WriteLine($"Запис [{result.Id}] Аналіз додано.");
    }
    catch (ArgumentOutOfRangeException ex)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
    }
}

static void AddPrescriptionUI(Clinic clinic)
{
    clinic.Patients.DisplayAll();
    clinic.Doctors.DisplayAll();

    Console.Write("ID пацієнта: ");
    if (!int.TryParse(Console.ReadLine(), out int patientId))
    {
        Console.WriteLine("Помилка: Некоректне число.");
        return;
    }

    Console.Write("ID лікаря: ");
    if (!int.TryParse(Console.ReadLine(), out int doctorId))
    {
        Console.WriteLine("Помилка: Некоректне число.");
        return;
    }

    if (clinic.Patients.FindById(patientId) == null || clinic.Doctors.FindById(doctorId) == null)
    {
        Console.WriteLine("Помилка: Пацієнта чи лікаря не знайдено.");
        return;
    }

    Console.Write("Препарат: ");
    string medication = Console.ReadLine() ?? "";

    Console.Write("Дозування (10 мг): ");
    string dosage = Console.ReadLine() ?? "";

    Console.Write("Кількість днів (ціле): ");
    if (!int.TryParse(Console.ReadLine(), out int durationDays))
    {
        Console.WriteLine("Помилка: Некоректне число.");
        return;
    }

    Console.Write("Інструкція (Enter - пропустити): ");
    string instructions = Console.ReadLine() ?? "";

    try
    {
        Prescription rx = new Prescription(patientId, doctorId, DateTime.Today, medication, dosage, durationDays, instructions);
        clinic.MedicalRecords.Add(rx);
        Console.WriteLine($"Запис [{rx.Id}] Рецепт додано.");
    }
    catch (ArgumentOutOfRangeException ex)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
    }
}

static void ShowDoctorRecordsUI(Clinic clinic)
{
    clinic.Doctors.DisplayAll();
    Console.Write("ID лікаря: ");
    if (!int.TryParse(Console.ReadLine(), out int doctorId))
    {
        Console.WriteLine("Помилка: Некоректне число.");
        return;
    }

    Doctor? doctor = clinic.Doctors.FindById(doctorId);
    if (doctor == null)
    {
        Console.WriteLine("Помилка: Лікаря не знайдено.");
        return;
    }

    MedicalRecord[] records = clinic.MedicalRecords.GetByDoctorId(doctorId);
    clinic.MedicalRecords.DisplayList(records);
}