namespace ClinicApp.Utils;

public class ValidationResult
{
    private readonly List<string> _errors = new();
    public bool IsValid => _errors.Count == 0;
    public IReadOnlyList<string> Errors => _errors.AsReadOnly();

    public void AddError(string error)
    {
        if (!string.IsNullOrWhiteSpace(error))
        {
            _errors.Add(error);
        }
    }

    public void Print()
    {
        foreach (var error in Errors)
        {
            Console.WriteLine($"[!]{error}");
        }
    }
}