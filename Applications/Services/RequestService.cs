using Domain.Entities;
using Infrastructure;
using transport_management_system.Domain.Entities;
using transport_management_system.Domain;
using transport_management_system.Domain.Exceptions;
using Domain.Interfaces;

namespace Applications.Services;

public class RequestService
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
        var driver = _driverRepository.GetById(driverId);

        if (driver == null)
        {
            throw new DriverNotFoundExcpetion(driverId);
        }
        driver.CheckAvailability();

        var request = new Request(vehicleId, driverId, distance);

        decimal? costPerKm = _vehicleRepository.GetCostPerKmById(vehicleId);
        decimal? salaryPerKm = _driverRepository.GetSalaryPerKmById(driverId);

        decimal totalCost = 0;

        if (costPerKm.HasValue && salaryPerKm.HasValue)
        {
            totalCost = request.Distance * costPerKm.Value + request.Distance * salaryPerKm.Value;
        }

        request.SetTotalCost(totalCost);
        _driverRepository.ChangeAvailabilityToZeroById(driverId);

        return request;
    }


}