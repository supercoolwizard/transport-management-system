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

        var processedRequest_1 = requestService.ProcessRequest(1, 1, 12);
        // Console.WriteLine($"The total cost of your transportation will be {processedRequest_1.TotalCost}");

        var processedRequest_2 = requestService.ProcessRequest(1, 1, 22);
        // Console.WriteLine($"The total cost of your transportation will be {processedRequest_2.TotalCost}");
     
               

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