using transport_management_system.Domain.Entities;

namespace Domain.Interfaces;

public interface IDriverRepository
{
    IEnumerable<Driver> GetAll(); // returns all drivers
    Driver? GetById(int id); // returns one driver by ID
    decimal? GetSalaryPerKmById(int id);
}

