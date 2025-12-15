using System.Diagnostics;

using transport_management_system.Applications.Services;
using transport_management_system.Applications.Decorators;
using transport_management_system.Domain.Interfaces;
using transport_management_system.Infrastructure.Repositories;

namespace transport_management_system;

class Program
{
    static void Main(string[] args)
    {
        string driversCsvPath = "Infrastructure/Files/drivers.csv";
        string vehiclesCsvPath = "Infrastructure/Files/vehicles.csv";    
        
        IDriverRepository driversRepository = new DriverRepository(driversCsvPath);
        IVehicleRepository vehiclesRepository = new VehicleRepository(vehiclesCsvPath);

        IRequestService preRequestService = new RequestService(vehiclesRepository, driversRepository);
        IRequestService requestService = new RequestServiceExceptionHandlerDecorator(preRequestService);

        var processedRequest_1 = requestService.ProcessRequest(1, 1, 12);  // (vehicleId, driverId, distance)
        var processedRequest_2 = requestService.ProcessRequest(2, 1, 22);  // driver unavailable
        var processedRequest_3 = requestService.ProcessRequest(1, 3, 23);  // vehicle unavailable

        string pythonExe = "python3";
        string scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Python", "distance_calculator.py");

        string city1 = "Kyiv";
        string city2 = "Dnipro";

        var psi = new ProcessStartInfo
        {
            FileName = pythonExe,
            Arguments = $"{scriptPath} {city1} {city2}",
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
            Console.WriteLine("Python error: " + error);
        else
            Console.WriteLine("Python output: " + output);



    }
}