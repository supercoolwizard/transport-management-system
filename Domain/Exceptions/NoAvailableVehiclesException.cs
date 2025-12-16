

namespace transport_management_system.Domain.Exceptions;

public class NoAvailableVehiclesException : InvalidOperationException
{
    public NoAvailableVehiclesException(string message) : base(message) { }
}