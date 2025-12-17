using System.Diagnostics;

using transport_management_system.Domain.Interfaces;
using transport_management_system.Domain.Exceptions;

namespace transport_management_system.Applications.Services;

public class PythonDistanceService : IDistanceService
{
    public decimal CalculateDistance(string from, string to)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "./venv/bin/python",
            Arguments = $"Python/distance_calculator.py \"{from}\" \"{to}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(psi);

        string stdout = process!.StandardOutput.ReadToEnd();
        string stderr = process.StandardError.ReadToEnd();

        process.WaitForExit();

        if (process.ExitCode == 0)
            return decimal.Parse(stdout.Trim());

        if (process.ExitCode == 21 && stderr.StartsWith("CITY_NOT_FOUND|"))
            throw new CityNotFoundException(stderr.Split('|', 2)[1]);
        if (process.ExitCode == 22 && stderr.StartsWith("CITY_NOT_IN_UKRAINE|"))
            throw new CityNotInUkraineException(stderr.Split('|', 2)[1]);

        if (process.ExitCode == 30)
            throw new TimeoutException("Geocoding service timeout");
        if (process.ExitCode == 31)
            throw new Exception("Geocoding service unavailable");

        
        throw new Exception($"Python error: {stderr}");
    }
}
