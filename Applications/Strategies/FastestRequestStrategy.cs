using transport_management_system.Domain.Entities;
using transport_management_system.Domain.Interfaces;

namespace transport_management_system.Applications.Strategies;

public class FastestRequestStrategy : IRequestSelectionStrategy
{
    public (Driver, Vehicle) Select(IEnumerable<Driver> drivers, IEnumerable<Vehicle> vehicles)
    {
        var driver = drivers
            .Where(d => d.Availability == 1)
            .OrderByDescending(d => d.SalaryPerKm)
            .First();
        
        var vehicle = vehicles
            .Where(v => v.Availability == 1)
            .OrderByDescending(d => d.CostPerKm)
            .First();

        return (driver, vehicle);
    }
}