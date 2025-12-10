
namespace transport_management_system.Domain.Exceptions;

public class DriverNotFoundExcpetion : Exception
{
    public int DriverId { get; }

    public DriverNotFoundExcpetion(int driverId) 
        : base($"Driver with ID {driverId} was not found.")
    {
        DriverId = driverId;
    }

    public DriverNotFoundExcpetion(int driverId, string message) 
        : base(message)
    {
        DriverId = driverId;
    }

}