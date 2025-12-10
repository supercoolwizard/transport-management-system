using Applications.Services;
using Domain.Entities;
using transport_management_system.Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;
using transport_management_system.Domain.Exceptions;

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

        try
        {
            var processedRequest_1 = requestService.ProcessRequest(1, 1, 12);
            Console.WriteLine($"The total cost of your transportation will be {processedRequest_1.TotalCost}");
        }
        catch (DriverUnavailableException)
        {
            Console.WriteLine("1 Skipped");
        }
        try
        {
            var processedRequest_2 = requestService.ProcessRequest(1, 1, 22);
            Console.WriteLine($"The total cost of your transportation will be {processedRequest_2.TotalCost}");
        }
        catch (DriverUnavailableException)
        {
            Console.WriteLine("2 Skipped");
        }

        
        
        
       

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