

namespace transport_management_system.Domain.Entities;

public class Driver
{
    public int DriverId { get; }
    public string Name { get; }
    public decimal SalaryPerKm { get; }

    public Driver(int driverId, string name, decimal salaryPerKm)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Driver name cannot be empty.", nameof(name));
        
        if (salaryPerKm < 0)
            throw new ArgumentException("Salary per km cannot be negative.", nameof(salaryPerKm));
        
        DriverId = driverId;
        Name = name;
        SalaryPerKm = salaryPerKm;
    }

}