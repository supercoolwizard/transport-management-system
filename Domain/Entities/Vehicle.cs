

using transport_management_system.Domain.Exceptions;

namespace transport_management_system.Domain.Entities;

public class Vehicle 
{
    public int VehicleId { get; }
    public string Name { get; }
    public decimal CostPerKm { get; }
    public int Availability { get;}

    public Vehicle(int vehicleId, string name, decimal costPerKm, int availability)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Vehicle name cannot be empty.", nameof(name));
        
        if (costPerKm < 0)
            throw new ArgumentException("Salary per km cannot be negative.", nameof(costPerKm));
        
        VehicleId = vehicleId;
        Name = name;
        CostPerKm = costPerKm;
        Availability = availability;
    }

    public void CheckAvailability()
    {
        if (Availability == 0)
        {
            throw new VehicleUnavailableException
            (
                VehicleId,
                Name,
                $"Vehicle with ID {VehicleId} ({Name}) is currently unavailable"
            );
        }
    }

}