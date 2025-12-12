using transport_management_system.Domain.Entities;
using transport_management_system.Domain.Interfaces;
using transport_management_system.Domain.Exceptions;

namespace transport_management_system.Infrastructure.Repositories;

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
                decimal.Parse(cols[2]),
                int.Parse(cols[3])
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

    public void ChangeAvailabilityToZeroById(int id)
    {
        var lines = File.ReadAllLines(_filePath).ToList();

        for (int i = 1; i < lines.Count; i++)
        {
            var cols = lines[i].Split(',');
            if (int.Parse(cols[0]) == id)
            {
                cols[3] = "0";
                lines[i] = string.Join(",", cols);

                File.WriteAllLines(_filePath, lines);
                return;
            }
        }
        throw new VehicleUnavailableException(id, $"Vehicle with ID {id} was not found for update.");
    }
}