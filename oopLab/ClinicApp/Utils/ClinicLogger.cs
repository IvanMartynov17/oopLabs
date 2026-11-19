using System;
using System.IO;
using System.Text;

namespace ClinicApp.Utils;

public class ClinicLogger
{
    private readonly string _logPath;

    public ClinicLogger(string logPath = "ClinicApp/clinic.log")
    {
        if (string.IsNullOrWhiteSpace(logPath))
        {
            throw new ArgumentException("Шлях до файлу лога не може бути порожнім.", nameof(logPath));
        }

        _logPath = logPath;
    }
    
    public void LogInfo(string message) => WriteEntry("INFO ", message);
    public void LogWarning(string message) => WriteEntry("WARNING ", message);
    public void LogError(string message) => WriteEntry("ERROR ", message);

    public string[] GetLastLines(int count)
    {
        if (count <= 0 || !Exists())
        {
            return Array.Empty<string>();
        }
        string[] allLines = File.ReadAllLines(_logPath, Encoding.UTF8);
        int skipCount = Math.Max(0, allLines.Length - count);
        int resultLength = Math.Min(allLines.Length, count);
        
        string[] result = new string[resultLength];
        for (int i = 0; i < resultLength; i++)
        {
            result[i] = allLines[skipCount + i];
        }
        
        return result;
    }

    public void Clear()
    {
        File.WriteAllText(_logPath, string.Empty, Encoding.UTF8);
    }

    public bool Exists()
    {
        return File.Exists(_logPath);
    }

    private void WriteEntry(string level, string message)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string line = $"[{timestamp}] [{level}] {message}{Environment.NewLine}";
        File.AppendAllText(_logPath, line, Encoding.UTF8);
    }
}