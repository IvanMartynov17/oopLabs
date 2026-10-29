using ClinicApp.Models;
using ClinicApp.Enums;
using ClinicApp.Interfaces;

namespace ClinicApp.Managers;
public class AppointmentManager
{
    private const int MaxAppointments = 500;
    private Appointment[] _appointments = new Appointment[MaxAppointments];
    private int _count = 0;

    private PatientManager _patients;
    private DoctorManager _doctors;

    public int Count
    {
        get { return _count; }
    }

    public AppointmentManager(PatientManager patients, DoctorManager doctors)
    {
        _patients = patients;
        _doctors = doctors;
    }

    public bool Book(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30)
    {
        if (_count >= MaxAppointments)
        {
            Console.WriteLine("Досягнуто ліміт записів.");
            return false;
        }

        Patient? patient = _patients.FindById(patientId);
        if (patient == null)
        {
            Console.WriteLine($"Помилка: пацієнта з ID {patientId} не знайдено.");
            return false;
        }

        Doctor? doctor = _doctors.FindById(doctorId);
        if (doctor == null)
        {
            Console.WriteLine($"Помилка: лікаря з ID {doctorId} не знайдено.");
            return false;
        }

        Appointment appointment = new RegularAppointment(patientId, doctorId, scheduledAt, durationMinutes);
        _appointments[_count] = appointment;
        _count++;

        Console.WriteLine($"Запис [{appointment.Id}] створено: {patient.FullName} → {doctor.FullName} о {scheduledAt:dd.MM.yyyy HH:mm}");
        return true;
    }

    public bool Cancel(int id, string reason = "")
    {
        Appointment? found = FindById(id);
        if (found == null)
        {
            return false;
        }

        return found.Cancel(reason);
    }

    public bool Complete(int id)
    {
        Appointment? found = FindById(id);
        if (found == null)
        {
            return false;
        }

        return found.Complete();
    }

    public Appointment[] GetByPatient(int patientId)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].PatientId == patientId) matches++;
        }

        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].PatientId == patientId)
            {
                result[index] = _appointments[i];
                index++;
            }
        }
        return result;
    }

    public Appointment[] GetByDoctor(int doctorId)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DoctorId == doctorId) matches++;
        }

        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DoctorId == doctorId)
            {
                result[index] = _appointments[i];
                index++;
            }
        }
        return result;
    }

    public Appointment[] GetByDate(DateTime date)
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].ScheduledAt.Date == date.Date) matches++;
        }

        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].ScheduledAt.Date == date.Date)
            {
                result[index] = _appointments[i];
                index++;
            }
        }
        return result;
    }

    public Appointment[] GetUpcoming()
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].IsUpcoming) matches++;
        }

        Appointment[] result = new Appointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].IsUpcoming)
            {
                result[index] = _appointments[i];
                index++;
            }
        }
        return result;
    }
    public void DisplayList(Appointment[] appointments)
    {
        if (appointments.Length == 0)
        {
            Console.WriteLine("Не знайдено.");
            return;
        }

        for (int i = 0; i < appointments.Length; i++)
        {
            DisplayAppointment(appointments[i]);
        }
    }

    private Appointment? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].Id == id)
            {
                return _appointments[i];
            }
        }
        return null;
    }
    
    public Appointment[] GetByDate(int year, int month, int day)
    {
        return GetByDate(new DateTime(year, month, day)); // Делегує на GetByDate(DateTime)[cite: 3]
    }
    
    public Appointment[] GetAll()
    {
        Appointment[] copy = new Appointment[Count];
            
        for (int i = 0; i < Count; i++)
        {
            copy[i] = _appointments[i];
        }
            
        return copy;
    }
    
    public static int CancelAll(ICancellable[] items, string reason = "")
    {
        int cancelledCount = 0;

        foreach (var item in items)
        {
            if (item.Cancel(reason))
            {
                cancelledCount++;
            }
        }

        return cancelledCount;
    }

    public UrgentAppointment BookUrgent(int patientId, int doctorId, DateTime scheduledAt, string urgencyNote = "",
        int durationMinutes = 30)
    {
        if (_count >= MaxAppointments)
        {
            Console.Write("Досягнуто ліміту");
            return null!;
        }
        Patient? patient = _patients.FindById(patientId);
        if (patient == null)
        {
            Console.WriteLine($"Помилка: пацієнта з ID {patientId} не знайдено.");
            return null!;
        }

        Doctor? doctor = _doctors.FindById(doctorId);
        {
            if (doctor == null)
            {
                Console.Write($"Лікаря з {doctorId} не знайдено.");
                return null!;
            }
        }
        
        UrgentAppointment appointment = new UrgentAppointment(patientId, doctorId, scheduledAt, urgencyNote, durationMinutes);
        _appointments[_count] = appointment;
        _count++;

        Console.WriteLine($"Запис [{appointment.Id}] створено (Терміновий): {patient.FullName} → {doctor.FullName} о {scheduledAt:dd.MM.yyyy HH:mm}");
        return appointment;
    }

    public SpecialistAppointment BookSpecialist(int patientId, int doctorId, DateTime scheduledAt, string topic = "",
        int durationMinutes = 45)
    {
        if (_count >= MaxAppointments)
        {
            Console.WriteLine("Досягнуто ліміт записів.");
            return null!;
        }

        Patient? patient = _patients.FindById(patientId);
        if (patient == null)
        {
            Console.WriteLine($"Помилка: пацієнта з ID {patientId} не знайдено.");
            return null!;
        }

        Doctor? doctor = _doctors.FindById(doctorId);
        if (doctor == null)
        {
            Console.WriteLine($"Помилка: лікаря з ID {doctorId} не знайдено.");
            return null!;
        }

        SpecialistAppointment appointment = new SpecialistAppointment(patientId, doctorId, scheduledAt, topic, durationMinutes);
        _appointments[_count] = appointment;
        _count++;

        Console.WriteLine($"Запис [{appointment.Id}] створено (Спеціаліст): {patient.FullName} → {doctor.FullName} о {scheduledAt:dd.MM.yyyy HH:mm}");
        return appointment;
    }
    
    public UrgentAppointment[] GetUrgent()
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i] is UrgentAppointment) matches++;
        }

        UrgentAppointment[] result = new UrgentAppointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i] is UrgentAppointment urgent)
            {
                result[index] = urgent;
                index++;
            }
        }
        return result;
    }
    
    public SpecialistAppointment[] GetSpecialist()
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i] is SpecialistAppointment) matches++;
        }

        SpecialistAppointment[] result = new SpecialistAppointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i] is SpecialistAppointment specialist)
            {
                result[index] = specialist;
                index++;
            }
        }
        return result;
    }
    
    public RegularAppointment[] GetRegular()
    {
        int matches = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i] is RegularAppointment) matches++;
        }

        RegularAppointment[] result = new RegularAppointment[matches];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i] is RegularAppointment regular)
            {
                result[index] = regular;
                index++;
            }
        }
        return result;
    }
    
    public void DisplayAppointment(Appointment appointment)
    {
        Patient? patient = _patients.FindById(appointment.PatientId);
        Doctor? doctor = _doctors.FindById(appointment.DoctorId);

        string patientName = patient != null ? patient.FullName : $"Пацієнт #{appointment.PatientId}";
        string doctorName = doctor != null ? doctor.FullName : $"Лікар #{appointment.DoctorId}";

        string line = $"[{appointment.Id}] {appointment.GetDescription()} | {patientName} → {doctorName} | " +
                      $"{appointment.ScheduledAt:dd.MM.yyyy HH:mm}–{appointment.EndsAt:HH:mm} | {appointment.Status}";

        if (appointment.Notes.Length > 0)
        {
            line += $" | {appointment.Notes}";
        }

        line += $" | {appointment.GetCost():F2} грн";

        Console.WriteLine(line);
    }
}