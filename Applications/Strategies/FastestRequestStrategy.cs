using System.Security.Cryptography;

using transport_management_system.Domain.Entities;
using transport_management_system.Domain.Exceptions;
using transport_management_system.Domain.Interfaces;

namespace transport_management_system.Applications.Strategies;

public class FastestRequestStrategy : IRequestSelectionStrategy
{
    public (Driver, Vehicle) Select(IEnumerable<Driver> drivers, IEnumerable<Vehicle> vehicles)
    {
        var availableDrivers = drivers.Where(d => d.Availability == 1).ToList();
        if (!availableDrivers.Any())
            throw new NoAvailableDriversException("No available drivers.");

        var availableVehicles = vehicles.Where(d => d.Availability == 1);
        if (!availableVehicles.Any())
            throw new NoAvailableVehiclesException("No available vehicles.");
        
        var driver = availableDrivers[
            RandomNumberGenerator.GetInt32(availableDrivers.Count)
        ];
        
        var vehicle = availableVehicles
            .OrderByDescending(v => v.MaxSpeed)
            .First();

        return (driver, vehicle);
    }
}