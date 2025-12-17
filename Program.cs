using System.Diagnostics;
using Microsoft.Extensions.Logging;

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
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .SetMinimumLevel(LogLevel.Information)
                .AddConsole();
        });

        ILogger logger = loggerFactory.CreateLogger("App");
        
        string driversCsvPath = "Infrastructure/Files/drivers.csv";
        string vehiclesCsvPath = "Infrastructure/Files/vehicles.csv";    
        
        IDriverRepository driversRepository = new DriverRepository(driversCsvPath);
        IVehicleRepository vehiclesRepository = new VehicleRepository(vehiclesCsvPath);
        
        var requestServiceLogger = loggerFactory.CreateLogger<RequestServiceExceptionHandlerDecorator>();

        IRequestService preRequestService = new RequestService(vehiclesRepository, driversRepository);
        IRequestService requestService = new RequestServiceExceptionHandlerDecorator(preRequestService, requestServiceLogger);


        IDistanceService distanceService = new PythonDistanceService();

        var distanceLogger = loggerFactory.CreateLogger<PythonDistanceServiceExceptionHandlerDecorator>();

        distanceService = new PythonDistanceServiceExceptionHandlerDecorator(distanceService, distanceLogger);

        // var processedRequest_1 = requestService.ProcessRequest(1, 1, 12);  // (vehicleId, driverId, distance)
        // var processedRequest_2 = requestService.ProcessRequest(2, 1, 22);  // driver unavailable
        // var processedRequest_3 = requestService.ProcessRequest(1, 3, 23);  // vehicle unavailable

        var strategyLogger = loggerFactory.CreateLogger<RequestSelectionStrategyExceptionHandlerDecorator>();
        IRequestSelectionStrategy strategy = new RequestSelectionStrategyExceptionHandlerDecorator(
            new CheapestRequestStrategy(),
            strategyLogger
        );

        var facade = new RequestFacade(
            driversRepository,
            vehiclesRepository,
            distanceService,
            strategy,
            requestService
        );

        var processedRequestThroughFacade_1 = facade.CreateRequest("Kyiv", "Lviv");
        Console.WriteLine($"Total cost: {processedRequestThroughFacade_1.TotalCost:C}, Driver: {processedRequestThroughFacade_1.DriverName}, Vehicle: {processedRequestThroughFacade_1.VehicleName}");
        // var processedRequestThroughFacade_2 = facade.CreateRequest("London", "Lviv");
        // Console.WriteLine($"Total cost: {processedRequestThroughFacade_2.TotalCost:C}, Driver: {processedRequestThroughFacade_2.DriverName}, Vehicle: {processedRequestThroughFacade_2.VehicleName}");

    }
}