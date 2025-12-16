using System.Diagnostics;

using transport_management_system.Domain.Interfaces;

namespace transport_management_system.Applications.Services;

public class PythonDistanceService : IDistanceService
{
    public decimal CalculateDistance(string from, string to)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "python3",
            Arguments = $"Python/distance_calculator.py \"{from}\" \"{to}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(psi);

        string output = process!.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        process.WaitForExit();

        if (!string.IsNullOrWhiteSpace(error))
            throw new Exception(error);
        
        return decimal.Parse(output.Trim());
    }
}
