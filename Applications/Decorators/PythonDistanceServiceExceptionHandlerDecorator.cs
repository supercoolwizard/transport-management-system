using Microsoft.Extensions.Logging;
using transport_management_system.Domain.Exceptions;
using transport_management_system.Domain.Interfaces;

namespace transport_management_system.Applications.Decorators;
public class PythonDistanceServiceExceptionHandlerDecorator : IDistanceService
{
    private readonly IDistanceService _inner;
    private readonly ILogger<PythonDistanceServiceExceptionHandlerDecorator> _logger;

    public PythonDistanceServiceExceptionHandlerDecorator(
        IDistanceService inner, 
        ILogger<PythonDistanceServiceExceptionHandlerDecorator> logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public decimal CalculateDistance(string from, string to)
    {
        try
        {
            _logger.LogInformation(
                "Calculating distance between {from} and {to}",
                from, to
            );

            return _inner.CalculateDistance(from, to);
        }
        catch (CityNotFoundException ex)
        {
            _logger.LogWarning(
                ex,
                "City not found: {from} or {to}",
                from, to
            );
            throw;
        }
        catch (CityNotInUkraineException ex)
        {
            _logger.LogWarning(
                ex,
                "City not found in Ukraine: {from} or {to}",
                from, to
            );
            throw;
        }
        catch (TimeoutException ex)
        {
            _logger.LogError(
                ex,
                "Geocoding service timeout for {from} or {to}",
                from, to
            );
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(
                ex,
                "Python environment failure while calculating distance"
            );
            throw;
        }
    }
}
