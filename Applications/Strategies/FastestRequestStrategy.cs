using transport_management_system.Domain.Entities;
using transport_management_system.Domain.Exceptions;
using transport_management_system.Domain.Interfaces;

namespace transport_management_system.Applications.Strategies;

public class FastestRequestStrategy : IRequestSelectionStrategy
{
    public (Driver, Vehicle) Select(IEnumerable<Driver> drivers, IEnumerable<Vehicle> vehicles)
    {
        var availableDrivers = drivers.Where(d => d.Availability == 1);
        if (!availableDrivers.Any())
            throw new NoAvailableDriversException("No available drivers.");

        var availableVehicles = vehicles.Where(d => d.Availability == 1);
        if (!availableDrivers.Any())
            throw new NoAvailableVehiclesException("No available vehicles.");
        
        var driver = availableDrivers
            .OrderByDescending(d => d.SalaryPerKm)
            .First();
        
        var vehicle = availableVehicles
            .OrderByDescending(d => d.CostPerKm)
            .First();

        return (driver, vehicle);
    }
}