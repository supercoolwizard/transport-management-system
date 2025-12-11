using transport_management_system.Domain.Entities;
using transport_management_system.Domain.Interfaces;
using transport_management_system.Domain.Exceptions;

namespace transport_management_system.Infrastructure.Repositories;

public class DriverRepository : IDriverRepository
{
    private readonly string _filePath;

    public DriverRepository(string filePath)
    {
        _filePath = filePath;
    }

    public IEnumerable<Driver> GetAll()
    {
        return File.ReadAllLines(_filePath)
            .Skip(1)  // header
            .Select(line => line.Split(','))
            .Select(cols => new Driver(
                int.Parse(cols[0]),
                cols[1],
                decimal.Parse(cols[2]),
                int.Parse(cols[3])
            ));
    }

    public Driver? GetById(int id)
    {
        return GetAll().FirstOrDefault(d => d.DriverId == id);
    }

    public decimal? GetSalaryPerKmById(int id)
    {
        Driver? driver = GetById(id);

        return driver?.SalaryPerKm;
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
        throw new DriverUnavailableException(id, $"Driver with ID {id} was not found for update.");
    }
}