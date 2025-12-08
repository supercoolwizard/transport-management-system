using Applications.Services;
using Domain.Entities;
using transport_management_system.Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace transport_management_system;

class Program
{
    static void Main(string[] args)
    {
        string driversCsvPath = "Infrastructure/Files/drivers.csv";
        string vehiclesCsvPath = "Infrastructure/Files/vehicles.csv";    
        
        IDriverRepository driversRepository = new DriverRepository(driversCsvPath);
        IVehicleRepository vehiclesRepository = new VehicleRepository(vehiclesCsvPath);
        
        var requestService = new RequestService(vehiclesRepository, driversRepository);
        var processedRequest = requestService.ProcessRequest(1, 1, 12);

        Console.WriteLine($"The total cost of your transportation will be {processedRequest.TotalCost}");
        

        // var allDrivers = driversRepo.GetAll();
        // Console.WriteLine("All drivers:");
        // foreach (var driver in allDrivers)
        // {
        //     Console.WriteLine($"{driver.DriverId} - {driver.Name} - {driver.SalaryPerKm}");
        // }

        // var allVehicles = vehiclesRepo.GetAll();
        // Console.WriteLine("All vehicles:");
        // foreach (var vehicle in allVehicles)
        // {
        //     Console.WriteLine($"{vehicle.DriverId} - {vehicle.Name} - {vehicle.SalaryPerKm}");
        // }
    }
}