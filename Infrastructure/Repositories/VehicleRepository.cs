using transport_management_system.Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly string _filePath;

    public VehicleRepository(string filePath)
    {
        _filePath = filePath;
    }

    public  IEnumerable<Vehicle> GetAll()
    {
        return File.ReadAllLines(_filePath)
            .Skip(1)  // header
            .Select(line => line.Split(','))
            .Select(cols => new Vehicle(
                int.Parse(cols[0]),
                cols[1],
                decimal.Parse(cols[2])
            ));
    }

    public Vehicle? GetById(int id)
    {
        return GetAll().FirstOrDefault(d => d.VehicleId == id);
    }

    public decimal? GetCostPerKmById(int id)
    {
        Vehicle? vehicle = GetById(id);

        return vehicle?.CostPerKm; 
    }
}