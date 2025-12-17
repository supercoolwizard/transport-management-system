using transport_management_system.Domain.Entities;
using transport_management_system.Domain.Exceptions;
using transport_management_system.Domain.Interfaces;

namespace transport_management_system.Applications.Strategies;

public class CheapestRequestStrategy : IRequestSelectionStrategy
{
    public (Driver, Vehicle) Select(IEnumerable<Driver> drivers, IEnumerable<Vehicle> vehicles)
    {
        var availableDrivers = drivers.Where(d => d.Availability == 1);
        if (!availableDrivers.Any())
            throw new NoAvailableDriversException("No available drivers.");

        var availableVehicles = vehicles.Where(d => d.Availability == 1);
        if (!availableVehicles.Any())
            throw new NoAvailableVehiclesException("No available vehicles.");

        var driver = availableDrivers
            .OrderBy(d => d.SalaryPerKm)
            .First();
    
        var vehicle = availableVehicles
            .OrderBy(v => v.CostPerKm)
            .First();

        // var availableDrivers = drivers.Where(d => d.Availability == 1).ToList();
        // var availableVehicles = vehicles.Where(v => v.Availability == 1).ToList();
        
        // Console.WriteLine("Available drivers:");
        // foreach (var d in availableDrivers)
        //     Console.WriteLine($"  {d.Name} – {d.SalaryPerKm}");

        // Console.WriteLine("Available vehicles:");
        // foreach (var v in availableVehicles)
        //     Console.WriteLine($"  {v.Name} – {v.CostPerKm}");

        // var driverr = availableDrivers.OrderBy(d => d.SalaryPerKm).First();
        // var vehiclee = availableVehicles.OrderBy(v => v.CostPerKm).First();

        // Console.WriteLine($"Selected driver: {driverr.Name}");
        // Console.WriteLine($"Selected vehicle: {vehiclee.Name}");


        return (driver, vehicle);
    }
}
