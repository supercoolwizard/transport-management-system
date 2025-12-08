using Domain.Interfaces;
using transport_management_system.Domain.Entities;
using transport_management_system.Domain.Interfaces;

namespace Infrastructure.Repositories;

public class DriverRepository : IVehicleRepository
{
    private readonly string _filePath;

    public DriverRepository(string filePath)
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
}