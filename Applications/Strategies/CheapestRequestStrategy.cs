using transport_management_system.Domain.Entities;
using transport_management_system.Domain.Interfaces;

namespace transport_management_system.Applications.Strategies;

public class CheapestRequestStrategy : IRequestSelectionStrategy
{
    public (Driver, Vehicle) Select(IEnumerable<Driver> drivers, IEnumerable<Vehicle> vehicles)
    {
        var driver = drivers
            .Where(d => d.Availability == 1)
            .OrderBy(d => d.SalaryPerKm)
            .First();
    
        var vehicle = vehicles
            .Where(d => d.Availability == 1)
            .OrderBy(d => d.CostPerKm)
            .First();
        
        return (driver, vehicle);
    }
}
