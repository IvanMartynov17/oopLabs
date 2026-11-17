using System;
using System.Globalization;
using System.Threading;
using ClinicApp;
using ClinicApp.Models;
using ClinicApp.Enums;
using ClinicApp.Managers;
using ClinicApp.Utils;
using ClinicApp.Comparators;
using ClinicApp.Interfaces;
using System.Collections.Generic;

Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

Clinic clinic = new Clinic("Медична Клініка");

Patient p1 = new Patient("Іван", "Петренко", new DateTime(1985, 8, 9), "A+", BloodType.APositive, "0501234567", "");
Patient p2 = new Patient("Олена", "Коваль", new DateTime(1993, 9, 10), "B-", BloodType.BNegative, "0672345678", "");
Patient p3 = new Patient("Максим", "Бойко", new DateTime(2010, 8, 9), "O+", BloodType.ONegative, "0933456789", "");

clinic.Patients.Add(p1);
clinic.Logger.LogInfo($"Додано пацієнта #{p1.Id}: {p1.FullName}");
clinic.Patients.Add(p2);
clinic.Logger.LogInfo($"Додано пацієнта #{p2.Id}: {p2.FullName}");
clinic.Patients.Add(p3);
clinic.Logger.LogInfo($"Додано пацієнта #{p3.Id}: {p3.FullName}");

Doctor d1 = new Doctor("Олег", "Сидоренко", Speciality.Cardiology, "LIC-001", "0441234567", 8, 16);
Doctor d2 = new Doctor("Наталія", "Мороз", Speciality.Neurology, "LIC-002", "0442345678", 9, 18);
Doctor d3 = new Doctor("Андрій", "Власенко", Speciality.Pediatrics, "LIC-003", "0443456789", 8, 17);

clinic.Doctors.Add(d1);
clinic.Logger.LogInfo($"Додано лікаря #{d1.Id}: {d1.FullName}");
clinic.Doctors.Add(d2);
clinic.Logger.LogInfo($"Додано лікаря #{d2.Id}: {d2.FullName}");
clinic.Doctors.Add(d3);
clinic.Logger.LogInfo($"Додано лікаря #{d3.Id}: {d3.FullName}");

clinic.MedicalRecords.Add(new Diagnosis(p1.Id, d1.Id, DateTime.Today.AddDays(-30), "I10", "Гіпертонічна хвороба", isChronic: true));
clinic.MedicalRecords.Add(new Diagnosis(p1.Id, d2.Id, DateTime.Today.AddDays(-5), "J06.9", "Гострий ринофарингіт", isChronic: false));
clinic.MedicalRecords.Add(new LabResult(p1.Id, d1.Id, DateTime.Today.AddDays(-3), "Гемоглобін", 145, "г/л", "120–160", isNormal: true));
clinic.MedicalRecords.Add(new LabResult(p1.Id, d1.Id, DateTime.Today.AddDays(-2), "Холестерин", 6.2, "ммоль/л", "< 5.2", isNormal: false));
clinic.MedicalRecords.Add(new Prescription(p1.Id, d1.Id, DateTime.Today.AddDays(-5), "Лізиноприл", "10 мг", 30, "1 раз на добу вранці"));

clinic.MedicalRecords.Add(new Prescription(p2.Id, d2.Id, DateTime.Today.AddDays(-40), "Амоксицилін", "500 мг", 10, "3 рази на день"));
clinic.MedicalRecords.Add(new LabResult(p2.Id, d2.Id, DateTime.Today.AddDays(-10), "Глюкоза", 5.0, "ммоль/л", "3.3–5.5", isNormal: true));

clinic.MedicalRecords.Add(new Diagnosis(p3.Id, d3.Id, DateTime.Today.AddDays(-1), "J00", "Гострий назофарингіт", isChronic: false));

if (clinic.Appointments.Book(p1.Id, d1.Id, DateTime.Today.AddDays(1).AddHours(10)))
{
    Appointment app1 = clinic.Appointments.GetByPatient(p1.Id)[^1];
    clinic.Logger.LogInfo($"Запис #{app1.Id} створено: пацієнт #{p1.Id} → лікар #{d1.Id}");
}

UrgentAppointment app2 = clinic.Appointments.BookUrgent(p2.Id, d2.Id, DateTime.Today.AddDays(1).AddHours(11), "біль у грудях");
clinic.Logger.LogInfo($"Запис #{app2.Id} створено: пацієнт #{p2.Id} → лікар #{d2.Id}");

SpecialistAppointment app3 = clinic.Appointments.BookSpecialist(p3.Id, d3.Id, DateTime.Today.AddDays(1).AddHours(12), "кардіологія", 60);
clinic.Logger.LogInfo($"Запис #{app3.Id} створено: пацієнт #{p3.Id} → лікар #{d3.Id}");

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
    Console.WriteLine("||  7. Черга                                    ||");
    Console.WriteLine("||  8. Аналітика                                ||");
    Console.WriteLine("||  9. Плани лікування - рефлексія, атрибути    ||");
    Console.WriteLine("|| 10. Файли                                    ||");
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
        case "7":
            WaitingRoomMenu(clinic);
            break;
        case "8":
            AnalyticsMenu(clinic);
            break;
        case "9":
            TreatmentPlansMenu(clinic);
            break;
        case "10":
            FilesMenu(clinic);
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

static void FilesMenu(Clinic clinic)
{
    while (true)
    {
        Console.WriteLine("\n-- Файли -------------------------------");
        Console.WriteLine("  1. Останні рядки лога");
        Console.WriteLine("  2. Очистити лог");
        Console.WriteLine("  0. Назад");
        Console.Write("Оберіть: ");

        string? choice = Console.ReadLine();
        Console.WriteLine();

        switch (choice)
        {
            case "1":
                Console.Write("Скільки останніх рядків показати (N): ");
                if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
                {
                    Console.WriteLine("Некоректне число.");
                    break;
                }

                string[] lines = clinic.Logger.GetLastLines(n);
                if (lines.Length == 0)
                {
                    Console.WriteLine("Лог порожній або файл ще не створено.");
                }
                else
                {
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                }

                break;

            case "2":
                clinic.Logger.Clear();
                Console.WriteLine("Лог очищено.");
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                break;
        }
    }
}

 static void TreatmentPlansMenu(Clinic clinic)
{
    while (true)
    {
        Console.WriteLine("\n── Плани лікування ────────────────────");
        Console.WriteLine("  1. Показати всі плани");
        Console.WriteLine("  2. Додати план лікування");
        Console.WriteLine("  3. Плани пацієнта");
        Console.WriteLine("  4. Активувати план");
        Console.WriteLine("  5. Завершити план");
        Console.WriteLine("  6. Скасувати план");
        Console.WriteLine("  7. Інформація про тип TreatmentPlan");
        Console.WriteLine("  0. Назад");
        Console.Write("Оберіть: ");

        string? choice = Console.ReadLine();
        Console.WriteLine();

        switch (choice)
        {
            case "1":
                DisplayTreatmentPlans(clinic.TreatmentPlans.GetAll());
                break;

            case "2":
                TreatmentPlan newPlan = FormBuilder.Build<TreatmentPlan>();
                if (clinic.TreatmentPlans.Add(newPlan))
                {
                    Console.WriteLine($"План лікування [{newPlan.Id}] успішно додано.");
                    clinic.Logger.LogInfo($"Додано план лікування #{newPlan.Id} для пацієнта #{newPlan.PatientId}");
                }
                break;

            case "3":
                Console.Write("ID пацієнта: ");
                if (!int.TryParse(Console.ReadLine(), out int patientId))
                {
                    Console.WriteLine("Некоректний ID пацієнта.");
                    break;
                }
                DisplayTreatmentPlans(clinic.TreatmentPlans.GetByPatient(patientId));
                break;

            case "4":
                ChangeTreatmentPlanStatus(clinic, "activate");
                break;

            case "5":
                ChangeTreatmentPlanStatus(clinic, "complete");
                break;

            case "6":
                ChangeTreatmentPlanStatus(clinic, "cancel");
                break;

            case "7":
                ModelValidator.PrintInfo(typeof(TreatmentPlan));
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                break;
        }
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
                    clinic.Logger.LogInfo($"Оплачено запис #{appointmentId}");
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
    if(cancelledCount > 0)
    {
        clinic.Logger.LogInfo($"Скасовано записів пацієнта #{patientId}: {cancelledCount}");
    }
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
        clinic.Logger.LogWarning($"Помилка введення: {ex.Message}");
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
        clinic.Logger.LogWarning($"Помилка введення: {ex.Message}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
        clinic.Logger.LogWarning($"Помилка введення: {ex.Message}");
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
        clinic.Logger.LogWarning($"Помилка введення: {ex.Message}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
        clinic.Logger.LogWarning($"Помилка введення: {ex.Message}");
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
        clinic.Logger.LogWarning($"Помилка введення: {ex.Message}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
        clinic.Logger.LogWarning($"Помилка введення: {ex.Message}");
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

static void AppointmentsMenu(Clinic clinic)
{
    bool back = false;
    while (!back)
    {
        Console.WriteLine("\n-- Записи ----------------------");
        Console.WriteLine("1 - Список майбутніх записів");
        Console.WriteLine("8 - Скасувати всі записи пацієнта");
        Console.WriteLine("9 - За типом прийому");
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
            case "9":
                ShowAppointmentsByTypeUI(clinic);
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

static void ShowAppointmentsByTypeUI(Clinic clinic)
{
    Console.WriteLine("1 - Термінові");
    Console.WriteLine("2 - Консультації спеціаліста");
    Console.WriteLine("3 - Звичайні");
    Console.Write("Оберіть тип: ");

    string? typeChoice = Console.ReadLine();
    Console.WriteLine();

    switch (typeChoice)
    {
        case "1":
            Appointment[] urgent = clinic.Appointments.GetUrgent();
            clinic.Appointments.DisplayList(urgent);
            break;
        case "2":
            Appointment[] specialist = clinic.Appointments.GetSpecialist();
            clinic.Appointments.DisplayList(specialist);
            break;
        case "3":
            Appointment[] regular = clinic.Appointments.GetRegular();
            clinic.Appointments.DisplayList(regular);
            break;
        default:
            Console.WriteLine("Некоректний вибір типу.");
            break;
    }
}

static void WaitingRoomMenu(Clinic clinic)
    {
        bool inSubMenu = true;

        while (inSubMenu)
        {
            Console.WriteLine("\n--- Черга --------------------");
            Console.WriteLine("1. Додати пацієнта до черги");
            Console.WriteLine("2. Прийняти першого");
            Console.WriteLine("3. Хто перший?");
            Console.WriteLine("4. Переглянути всю чергу");
            Console.WriteLine("0. Назад");
            Console.Write("Оберіть: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddPatientToQueue(clinic);
                    break;
                case "2":
                    DequeuePatient(clinic);
                    break;
                case "3":
                    PeekPatient(clinic);
                    break;
                case "4":
                    DisplayQueue(clinic);
                    break;
                case "0":
                    inSubMenu = false;
                    break;
                default:
                    Console.WriteLine("Невідомий пункт меню.");
                    break;
            }
        }
    }

static void AddPatientToQueue(Clinic clinic)
    {
        Console.Write("Введіть ID пацієнта: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            if (clinic.Patients.TryFindById(id, out Patient? patient) && patient != null)
            {
                clinic.WaitingRoom.Enqueue(patient);
                Console.WriteLine($"Пацієнта {patient.FullName} додано до черги.");
                clinic.Logger.LogInfo($"Додано пацієнта #{patient.Id}: {patient.FullName}");
            }
            else
            {
                Console.WriteLine("Пацієнта не знайдено.");
            }
        }
        else
        {
            Console.WriteLine("Некоректний ID.");
        }
    }

static void DequeuePatient(Clinic clinic)
    {
        try
        {
            Patient patient = clinic.WaitingRoom.Dequeue();
            Console.WriteLine($"Прийнято: {patient.FullName}. У черзі лишилось: {clinic.WaitingRoom.Count}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
            clinic.Logger.LogWarning($"Помилка введення: {ex.Message}");
        }
    }

    static void PeekPatient(Clinic clinic)
    {
        try
        {
            Patient patient = clinic.WaitingRoom.Peek();
            Console.WriteLine($"Перший у черзі: {patient.FullName}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
            clinic.Logger.LogWarning($"Помилка введення: {ex.Message}");
        }
    }

    static void DisplayQueue(Clinic clinic) 
    {
        Patient[] queueArray = clinic.WaitingRoom.ToArray();

        if (queueArray.Length == 0)
        {
            Console.WriteLine("Черга порожня.");
            return;
        }

        Console.WriteLine("\n--- Поточна черга ---");
        for (int i = 0; i < queueArray.Length; i++)
        {
            Console.WriteLine($"{i + 1}. [{queueArray[i].Id}] {queueArray[i].FullName} | Кров: {queueArray[i].BloodType}");
        }
        Console.WriteLine(new string('=', 30));
    }

static void AnalyticsMenu(Clinic clinic)
{
    while (true)
    {
        Console.WriteLine("\n── Аналітика ───────────────────");
        Console.WriteLine("  1. Лікарі за навантаженням");
        Console.WriteLine("  2. Лікарі за виручкою");
        Console.WriteLine("  3. Лікарі за іменем");
        Console.WriteLine("  4. Пацієнти за кількістю візитів");
        Console.WriteLine("  5. Пацієнти за витратами");
        Console.WriteLine("  0. Назад");
        Console.Write("Оберіть: ");

        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                var doctorsByLoad = CollectDoctorStats(clinic);
                doctorsByLoad.Sort();
                PrintStats(doctorsByLoad);
                break;

            case "2":
                var doctorsByRevenue = CollectDoctorStats(clinic);
                doctorsByRevenue.Sort(new DoctorStatsByRevenue());
                PrintStats(doctorsByRevenue);
                break;

            case "3":
                var doctorsByName = CollectDoctorStats(clinic);
                doctorsByName.Sort(new DoctorStatsByName());
                PrintStats(doctorsByName);
                break;

            case "4":
                var patientsByVisits = CollectPatientStats(clinic);
                patientsByVisits.Sort();
                PrintStats(patientsByVisits);
                break;

            case "5":
                var patientsBySpent = CollectPatientStats(clinic);
                patientsBySpent.Sort(new PatientStatsBySpent());
                PrintStats(patientsBySpent);
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                break;
        }
    }
}

static List<DoctorStats> CollectDoctorStats(Clinic clinic)
{
    var list = new List<DoctorStats>();
    foreach (DoctorStats stat in clinic.Analytics.ComputeDoctorStats())
    {
        list.Add(stat);
    }
    return list;
}

static List<PatientStats> CollectPatientStats(Clinic clinic)
{
    var list = new List<PatientStats>();
    foreach (PatientStats stat in clinic.Analytics.ComputePatientStats())
    {
        list.Add(stat);
    }
    return list;
}

static void PrintStats<T>(List<T> items)
{
    if (items.Count == 0)
    {
        Console.WriteLine("Дані відсутні.");
        return;
    }

    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}

static void DisplayTreatmentPlans(TreatmentPlan[] plans)
{
    if (plans.Length == 0)
    {
        Console.WriteLine("Планів лікування не знайдено.");
        return;
    }

    foreach (TreatmentPlan plan in plans)
    {
        Console.WriteLine(plan);
    }
}

static void ChangeTreatmentPlanStatus(Clinic clinic, string action)
{
    Console.Write("ID плану: ");
    if (!int.TryParse(Console.ReadLine(), out int planId))
    {
        Console.WriteLine("Некоректний ID плану.");
        return;
    }

    TreatmentPlan? plan = clinic.TreatmentPlans.GetById(planId);
    if (plan is null)
    {
        Console.WriteLine("План не знайдено.");
        return;
    }

    bool changed = action switch
    {
        "activate" => plan.Activate(),
        "complete" => plan.Complete(),
        "cancel" => plan.Cancel(),
        _ => false
    };

    if (changed)
    {
        Console.WriteLine($"Статус плану [{plan.Id}] оновлено: {plan.Status}.");
        clinic.Logger.LogInfo($"План лікування #{plan.Id} змінив статус на {plan.Status}");
    }
    else
    {
        Console.WriteLine($"Неможливо виконати перехід зі статусу {plan.Status}.");
    }
}


