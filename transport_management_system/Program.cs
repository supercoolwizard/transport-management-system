using transport_management_system.Domain.Entities;
using transport_management_system.Infrastructure.Repositories;

namespace transport_management_system;

class Program
{
    static void Main(string[] args)
    {
        string driverCsvPath = "Infrastructure/Files/drivers.csv";

        var driverRepo = new DriverRepository(driverCsvPath);

        var allDrivers = driverRepo.GetAll();
        Console.WriteLine("All drivers:");
        foreach (var driver in allDrivers)
        {
            Console.WriteLine($"{driver.DriverId} - {driver.Name} - {driver.SalaryPerKm}");
        }
    }
}