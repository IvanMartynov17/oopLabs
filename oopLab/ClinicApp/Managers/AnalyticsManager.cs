using ClinicApp.Models;
using System;
using System.Collections.Generic;

namespace ClinicApp.Managers;

public class AnalyticsManager
{
    private readonly AppointmentManager _appointments;
    private readonly DoctorManager _doctors;
    private readonly PatientManager _patients;

    public AnalyticsManager(AppointmentManager appointments, DoctorManager doctors, PatientManager patients)
    {
        _appointments = appointments ?? throw new ArgumentNullException(nameof(appointments));
        _doctors = doctors ?? throw new ArgumentNullException(nameof(doctors));
        _patients = patients ?? throw new ArgumentNullException(nameof(patients));
    }

    public IEnumerable<DoctorStats> ComputeDoctorStats()
    {
        var allAppointments = _appointments.GetAll();
        foreach (var doctor in _doctors.GetAll())
        {
            int count = 0;
            decimal revenue = 0m;
            DateTime lastDate = DateTime.MinValue;

            foreach (var appointment in allAppointments)
            {
                if (appointment.DoctorId == doctor.Id)
                {
                    count++;
                    revenue += appointment.GetCost();

                    if (appointment.ScheduledAt > lastDate)
                    {
                        lastDate = appointment.ScheduledAt;
                    }
                }
            }

            yield return new DoctorStats(doctor.Id, doctor.FullName, count, revenue, lastDate);
        }
    }

    public IEnumerable<PatientStats> ComputePatientStats()
    {
        var allAppointments = _appointments.GetAll();
        
        foreach (var patient in _patients.GetAll())
        {
            int count = 0;
            decimal spent = 0m;
            DateTime lastDate = DateTime.MinValue;
            
            foreach (var appointment in allAppointments)
            {
                if (appointment.PatientId == patient.Id)
                {
                    count++;
                    spent += appointment.GetCost();

                    if (appointment.ScheduledAt > lastDate)
                    {
                        lastDate = appointment.ScheduledAt;
                    }
                }
            }

            yield return new PatientStats(patient.Id, patient.FullName, count, spent, lastDate);
        }
    }
}