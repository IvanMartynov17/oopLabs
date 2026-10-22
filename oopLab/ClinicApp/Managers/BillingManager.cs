using System;
using ClinicApp.Interfaces;
using ClinicApp.Models;

namespace ClinicApp.Managers
{
    public class BillingManager
    {
        private readonly AppointmentManager _appointmentManager;

        public BillingManager(AppointmentManager appointmentManager)
        {
            _appointmentManager = appointmentManager ?? throw new ArgumentNullException(nameof(appointmentManager));
        }

        private IPayable[] FilterUnpaid(Appointment[] appointments)
        {
            int count = 0;

            for (int i = 0; i < appointments.Length; i++)
            {
                var app = appointments[i];
                if (app != null && !app.IsCancelled && !app.IsPaid)
                {
                    count++;
                }
            }

            IPayable[] result = new IPayable[count];
            int index = 0;

            for (int i = 0; i < appointments.Length; i++)
            {
                var app = appointments[i];
                if (app != null && !app.IsCancelled && !app.IsPaid)
                {
                    result[index++] = app;
                }
            }

            return result;
        }

        public IPayable[] GetAllUnpaid()
        {
            Appointment[] allAppointments = _appointmentManager.GetAll();
            return FilterUnpaid(allAppointments);
        }

        public IPayable[] GetUnpaidByPatient(int patientId)
        {
            Appointment[] patientAppointments = _appointmentManager.GetByPatient(patientId);
            return FilterUnpaid(patientAppointments);
        }

        public decimal GetTotalDebt()
        {
            IPayable[] unpaid = GetAllUnpaid();
            decimal total = 0m;

            foreach (var item in unpaid)
            {
                total += item.GetCost();
            }

            return total;
        }

        public decimal GetPatientDebt(int patientId)
        {
            IPayable[] unpaid = GetUnpaidByPatient(patientId);
            decimal total = 0m;

            foreach (var item in unpaid)
            {
                total += item.GetCost();
            }

            return total;
        }

        public bool PayAppointment(int appointmentId)
        {
            Appointment[] allAppointments = _appointmentManager.GetAll();

            foreach (var app in allAppointments)
            {
                if (app != null && app.Id == appointmentId)
                {
                    if (!app.IsCancelled && !app.IsPaid)
                    {
                        app.MarkPaid();
                        return true;
                    }
                    return false; 
                }
            }

            return false;
        }

        public void DisplayUnpaid(IPayable[] items)
        {
            if (items == null || items.Length == 0)
            {
                Console.WriteLine("Немає неоплачених записів.");
                return;
            }

            foreach (var item in items)
            {
                if (item is Appointment app)
                {
                    Console.WriteLine($"{app} | Сума: {item.GetCost():F2} грн");
                }
                else
                {
                    Console.WriteLine($"Запис | Сума: {item.GetCost():F2} грн");
                }
            }
        }
    }
}