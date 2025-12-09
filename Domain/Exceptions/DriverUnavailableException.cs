
namespace transport_management_system.Domain.Exceptions;

public class DriverUnavailableException : InvalidOperationException
{
    public int DriverId { get; }
    public string DriverName { get; }

    public DriverUnavailableException(int driverId, string driverName, string message) : base(message)
    {
        DriverId = driverId;
        DriverName = driverName;
    }
    
    public DriverUnavailableException(int driverId, string message)
    : this(driverId, "Unknown Driver", message){ }
}
