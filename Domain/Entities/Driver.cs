using transport_management_system.Domain.Exceptions;

namespace transport_management_system.Domain.Entities;

public class Driver
{
    public int DriverId { get; }
    public string Name { get; }
    public decimal SalaryPerKm { get; }
    public int Availability { get; }

    public Driver(int driverId, string name, decimal salaryPerKm, int availability)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainValidationException("Driver name cannot be empty.", nameof(name));
        
        if (salaryPerKm < 0)
            throw new DomainValidationException("Salary per km cannot be negative.", nameof(salaryPerKm));

        if (availability != 0 && availability != 1)
            throw new DomainValidationException("Availability must be 0 (unavailable) or 1 (available).", nameof(availability));
        
        DriverId = driverId;
        Name = name;
        SalaryPerKm = salaryPerKm;
        Availability = availability;
    }

    public void CheckAvailability()
    {
        if (Availability == 0)
        {
            throw new DriverUnavailableException(
                DriverId,
                Name,
                $"Driver with ID {DriverId} ({Name}) is currently unavailable."
            );
        }
    }

}