using transport_management_system.Applications.DTOs;
using transport_management_system.Domain.Entities;
using transport_management_system.Domain.Exceptions;
using transport_management_system.Domain.Interfaces;

namespace transport_management_system.Applications.Services;

public class RequestService : IRequestService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IDriverRepository _driverRepository;

    public RequestService(IVehicleRepository vehicleRepository, IDriverRepository driverRepository)
    {
        _vehicleRepository = vehicleRepository;
        _driverRepository = driverRepository;
    }

    public Request ProcessRequest(int vehicleId, int driverId, decimal distance) 
    {
        var driver = _driverRepository.GetById(driverId) ?? throw new DriverNotFoundExcpetion(driverId);
        driver.CheckAvailability();
        
        var vehicle = _vehicleRepository.GetById(vehicleId) ?? throw new VehicleNotFoundExcpetion(vehicleId);
        vehicle.CheckAvailability();

        var request = new Request(vehicleId, driverId, distance);

        decimal costPerKm = _vehicleRepository.GetCostPerKmById(vehicleId) ?? 0;
        decimal salaryPerKm = _driverRepository.GetSalaryPerKmById(driverId) ?? 0;

        request.SetTotalCost(distance * (costPerKm + salaryPerKm));

        _driverRepository.ChangeAvailabilityToZeroById(driverId);
        _vehicleRepository.ChangeAvailabilityToZeroById(vehicleId);

        return request;
    }


}