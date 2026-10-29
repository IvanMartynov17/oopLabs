using ClinicApp.Models;

namespace ClinicApp.Interfaces;


public interface IPayable
{
    decimal GetCost();
    bool IsPaid { get; }
    void MarkPaid();
}