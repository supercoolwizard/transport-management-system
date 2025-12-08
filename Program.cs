using transport_management_system.Domain.Entities;
using transport_management_system.Infrastructure.Repositories;

namespace transport_management_system;

class Program
{
    static void Main(string[] args)
    {
        string driverCsvPath = "Infrastructure/Files/vehicles.csv";        
        var vehiclesRepo = new DriverRepository(driverCsvPath);

        var allVehicles = vehiclesRepo.GetAll();
        Console.WriteLine("All vehicles:");
        foreach (var vehicle in allVehicles)
        {
            Console.WriteLine($"{vehicle.DriverId} - {vehicle.Name} - {vehicle.SalaryPerKm}");
        }
    }
}