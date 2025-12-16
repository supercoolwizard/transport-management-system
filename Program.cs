using System.Diagnostics;


using transport_management_system.Applications.Strategies;
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

        IDistanceService distanceService = new PythonDistanceService();

        // var processedRequest_1 = requestService.ProcessRequest(1, 1, 12);  // (vehicleId, driverId, distance)
        // var processedRequest_2 = requestService.ProcessRequest(2, 1, 22);  // driver unavailable
        // var processedRequest_3 = requestService.ProcessRequest(1, 3, 23);  // vehicle unavailable

        var strategy = new CheapestRequestStrategy();

        var facade = new RequestFacade(
            driversRepository,
            vehiclesRepository,
            distanceService,
            strategy
        );

        var request = facade.CreateRequest("Kyiv", "Lviv");
        var processedRequest_4 = requestService.ProcessRequest(request.VehicleId, request.DriverId, request.Distance);

    }
}