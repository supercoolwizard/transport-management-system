
namespace transport_management_system.Domain.Exceptions;

public class VehicleUnavailableException : InvalidOperationException
{
    public int VehicleId { get; }
    public string VehicleName { get; }

    public VehicleUnavailableException(int vehicleId, string vehicleName, string message) : base(message)
    {
        VehicleId = vehicleId;
        VehicleName = vehicleName;
    }
    
    public VehicleUnavailableException(int vehicleId, string message)
    : this(vehicleId, "Unknown Vehicle", message){ }
}
