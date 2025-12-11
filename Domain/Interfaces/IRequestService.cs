using transport_management_system.Domain.Entities;
using transport_management_system.Applications.Decorators;

namespace transport_management_system.Domain.Interfaces;

public interface IRequestService
{
    Request ProcessRequest(int vehicleId, int driverId, decimal distance);
}