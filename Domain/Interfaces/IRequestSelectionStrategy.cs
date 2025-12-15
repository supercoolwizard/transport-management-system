using transport_management_system.Domain.Entities;

namespace transport_management_system.Domain.Interfaces;

public interface IRequestSelectionStrategy
{
    (Driver driver, Vehicle vehcile) Select(
        IEnumerable<Driver> drivers,
        IEnumerable<Vehicle> vehicles
    );
}
