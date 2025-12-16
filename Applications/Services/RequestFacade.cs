using transport_management_system.Applications.Strategies;
using transport_management_system.Domain.Entities;
using transport_management_system.Domain.Interfaces;

namespace transport_management_system.Applications.Services;

public class RequestFacade
{
    private readonly IDriverRepository _drivers;
    private readonly IVehicleRepository _vehicles;
    private readonly IDistanceService _distanceService;
    private readonly IRequestSelectionStrategy _strategy;

    public RequestFacade(
        IDriverRepository drivers, 
        IVehicleRepository vehicles, 
        IDistanceService distanceService, 
        IRequestSelectionStrategy strategy)
    {
        _drivers = drivers ?? throw new ArgumentException(nameof(drivers));
        _vehicles = vehicles ?? throw new ArgumentException(nameof(vehicles));
        _distanceService = distanceService ?? throw new ArgumentException(nameof(distanceService));
        _strategy = strategy ?? throw new ArgumentException(nameof(strategy));
    }

    public Request CreateRequest(string from, string to)
    {
        var distance = _distanceService.CalculateDistance(from, to);

        var drivers = _drivers.GetAll();
        var vehicles = _vehicles.GetAll();
        
        var (driver, vehicle) = _strategy.Select(drivers, vehicles);

        return new(
            vehicle.VehicleId,
            driver.DriverId,
            distance
        );
    }
}