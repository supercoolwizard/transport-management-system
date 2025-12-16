
namespace transport_management_system.Domain.Exceptions;

public class NoAvailableDriversException : InvalidOperationException
{
    public NoAvailableDriversException(string message) : base(message) { }
}