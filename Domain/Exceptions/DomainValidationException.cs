
namespace transport_management_system.Domain.Exceptions;

public class DomainValidationException : ArgumentException
{
    public DomainValidationException(string message) : base(message) { }
    public DomainValidationException(string message, string paramName) : base(message, paramName) { }
}