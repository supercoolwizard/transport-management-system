using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

using transport_management_system.Applications.Strategies;
using transport_management_system.Applications.Services;
using transport_management_system.Applications.Decorators;
using transport_management_system.Domain.Interfaces;
using transport_management_system.Infrastructure.Repositories;
using transport_management_system.UI;

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

        var cli = new RequestCli(facade);
        cli.Run();
    }
}