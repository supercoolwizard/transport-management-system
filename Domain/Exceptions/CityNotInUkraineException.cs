namespace transport_management_system.Domain.Exceptions;

public class CityNotInUkraineException : DomainValidationException
{
    public CityNotInUkraineException(string message) : base(message) {}
}
