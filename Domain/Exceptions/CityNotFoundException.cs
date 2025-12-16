namespace transport_management_system.Domain.Exceptions;

public class CityNotFoundException : DomainValidationException
{
    public CityNotFoundException(string message) : base(message) {}
}
