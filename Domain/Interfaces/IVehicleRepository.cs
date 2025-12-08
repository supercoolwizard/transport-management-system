using transport_management_system.Domain.Entities;

namespace Domain.Interfaces;

public interface IVehicleRepository
{
    IEnumerable<Vehicle> GetAll();
    Vehicle? GetById(int id);
    decimal? GetCostPerKmById(int id);
}