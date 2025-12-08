using transport_management_system.Domain.Entities;
using transport_management_system.Domain;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class DriverRepository : IDriverRepository
{
    private readonly string _filePath;

    public DriverRepository(string filePath)
    {
        _filePath = filePath;
    }

    public  IEnumerable<Driver> GetAll()
    {
        return File.ReadAllLines(_filePath)
            .Skip(1)  // header
            .Select(line => line.Split(','))
            .Select(cols => new Driver(
                int.Parse(cols[0]),
                cols[1],
                decimal.Parse(cols[2])
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
}