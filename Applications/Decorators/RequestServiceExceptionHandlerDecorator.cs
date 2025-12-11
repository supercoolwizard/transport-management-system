using transport_management_system.Domain.Entities;
using transport_management_system.Domain.Interfaces;
using transport_management_system.Domain.Exceptions;

namespace transport_management_system.Applications.Decorators;

public class RequestServiceExceptionHandlerDecorator : IRequestService
{
    private readonly IRequestService _innerService;

    public RequestServiceExceptionHandlerDecorator(IRequestService innerService)
    {
        _innerService = innerService;
    }

    public Request ProcessRequest(int vehicleId, int driverId, decimal distance)
    {
        try
        {
            return _innerService.ProcessRequest(vehicleId, driverId, distance)!;
        }
        catch (DriverNotFoundExcpetion ex)
        {
            Console.WriteLine($"LOG: Driver not found for ID {ex.DriverId}. Error: {ex.Message}");
        }
        catch (DriverUnavailableException ex)
        {
            Console.WriteLine($"LOG: Driver unavailable. Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FATAL LOG: An unexpected error occurred: {ex.Message}");
            // throw new ApplicationException("An internal error occurred during request processing.", ex);
        }
        return new Request(vehicleId, driverId, distance)
        {
            TotalCost = 0
        };
 
    }

}
