
namespace transport_management_system.Domain.Exceptions;

public class VehicleNotFoundExcpetion : Exception
{
    public int VehicleId { get; }

    public VehicleNotFoundExcpetion(int vehicleId) 
        : base($"Vehicle with ID {vehicleId} was not found.")
    {
        VehicleId = vehicleId;
    }

    public VehicleNotFoundExcpetion(int vehicleId, string message) 
        : base(message)
    {
        VehicleId = vehicleId;
    }

}