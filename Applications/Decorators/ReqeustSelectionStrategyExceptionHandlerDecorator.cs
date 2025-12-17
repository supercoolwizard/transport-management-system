using Microsoft.Extensions.Logging;

using transport_management_system.Domain.Entities;
using transport_management_system.Domain.Exceptions;
using transport_management_system.Domain.Interfaces;

namespace transport_management_system.Applications.Decorators;

public class RequestSelectionStrategyExceptionHandlerDecorator : IRequestSelectionStrategy
{
    private readonly IRequestSelectionStrategy _inner;
    private readonly ILogger<RequestSelectionStrategyExceptionHandlerDecorator> _logger;

    public RequestSelectionStrategyExceptionHandlerDecorator(
        IRequestSelectionStrategy inner,
        ILogger<RequestSelectionStrategyExceptionHandlerDecorator> logger
    )
    {
        _inner = inner;
        _logger = logger;
    }

    public (Driver, Vehicle) Select(IEnumerable<Driver> drivers, IEnumerable<Vehicle> vehicles)
    {
        try
        {
            return _inner.Select(drivers, vehicles);
        }
        catch (NoAvailableDriversException ex)
        {
            _logger.LogWarning(ex, "No available drivers");
            throw;
        }
        catch (NoAvailableVehiclesException ex)
        {
            _logger.LogWarning(ex, "No available vehicles");
            throw;
        }
    }
}