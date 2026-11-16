using System;
using System.Collections.Generic;
using ClinicApp.Enums;
using ClinicApp.Models;
using ClinicApp.Utils;

namespace ClinicApp.Managers;

public class TreatmentPlanManager
{
    private readonly List<TreatmentPlan> _plans = new();

    public bool Add(TreatmentPlan plan)
    {
     ArgumentNullException.ThrowIfNull(plan);
     ValidationResult result = ModelValidator.Validate(plan);
     if (!result.IsValid)
     {
         result.Print();
         return false;
     }
     
     _plans.Add(plan);
     return true;
    }
    
    public TreatmentPlan? GetById(int id)
    {
        foreach (TreatmentPlan plan in _plans)
        {
            if (plan.Id == id)
            {
                return plan;
            }
        }
        return null;
    }

    public TreatmentPlan[] GetByPatient(int patientId)
    {
        var matches = new List<TreatmentPlan>();
        foreach (TreatmentPlan plan in _plans)
        {
            if (plan.PatientId == patientId)
            {
                matches.Add(plan);
            }
        }
        
        return matches.ToArray();
    }

    public TreatmentPlan[] GetByStatus(TreatmentStatus status)
    {
        var matches = new List<TreatmentPlan>();
        foreach (TreatmentPlan plan in _plans)
        {
            if (plan.Status == status)
            {
                matches.Add(plan);
            }
        }
        return matches.ToArray();
    }
    
    public TreatmentPlan[] GetAll()
    {
        return _plans.ToArray();
    }
}