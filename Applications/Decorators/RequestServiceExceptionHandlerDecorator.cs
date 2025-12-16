using Microsoft.Extensions.Logging;

using transport_management_system.Domain.Entities;
using transport_management_system.Domain.Interfaces;
using transport_management_system.Domain.Exceptions;

namespace transport_management_system.Applications.Decorators;

public class RequestServiceExceptionHandlerDecorator : IRequestService
{
    private readonly IRequestService _innerService;
    private readonly ILogger<RequestServiceExceptionHandlerDecorator> _logger;

    public RequestServiceExceptionHandlerDecorator(
        IRequestService innerService,
        ILogger<RequestServiceExceptionHandlerDecorator> logger)
    {
        _innerService = innerService;
        _logger = logger;
    }

    public Request ProcessRequest(int vehicleId, int driverId, decimal distance)
    {
        try
        {
            _logger.LogInformation(
                "Processing request: VehicleId={VehicleId}, DriverId={DriverId}, Distance={Distance}",
                vehicleId, driverId, distance
            );
            return _innerService.ProcessRequest(vehicleId, driverId, distance);
        }
        catch (DriverNotFoundExcpetion ex)
        {
            _logger.LogWarning(ex, "Driver not found: DriverId={DriverId}", ex.DriverId);
            throw;
        }
        catch (DriverUnavailableException ex)
        {
            _logger.LogWarning(ex, "Driver unavailable: DriverId={DriverId}", driverId);
            throw;
        }
        catch (VehicleNotFoundExcpetion ex)
        {
            _logger.LogWarning(ex, "Vehicle not found: VehicleId={VehicleId}", ex.VehicleId);
            throw;
        }
        catch (VehicleUnavailableException ex)
        {
            _logger.LogWarning(ex, "Vehicle unavailable: VehicleId={VehicleId}", vehicleId);
            throw;
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while processing request");
            throw;
        } 
    }

}
