

namespace transport_management_system.Domain.Entities;

class Vehicle 
{
    public int VehicleId { get; }
    public string Name { get; }
    public decimal CostPerKm { get; }

    public Vehicle(int vehicleId, string name, decimal costPerKm)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Vehicle name cannot be empty.", nameof(name));
        
        if (costPerKm < 0)
            throw new ArgumentException("Salary per km cannot be negative.", nameof(costPerKm));
        
        VehicleId = vehicleId;
        Name = name;
        CostPerKm = costPerKm;
    }

}