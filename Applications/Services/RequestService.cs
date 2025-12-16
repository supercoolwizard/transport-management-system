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
        var driver = _driverRepository.GetById(driverId);
        var vehicle = _vehicleRepository.GetById(vehicleId);

        if (driver == null)
        {
            throw new DriverNotFoundExcpetion(driverId);
        }
        driver.CheckAvailability();

        if (vehicle == null)
        {
            throw new VehicleNotFoundExcpetion(vehicleId);
        }
        vehicle.CheckAvailability();

        var request = new Request(vehicleId, driverId, distance);

        decimal? costPerKm = _vehicleRepository.GetCostPerKmById(vehicleId);
        decimal? salaryPerKm = _driverRepository.GetSalaryPerKmById(driverId);

        decimal totalCost = 0;

        if (costPerKm.HasValue && salaryPerKm.HasValue)
        {
            totalCost = request.Distance * costPerKm.Value + request.Distance * salaryPerKm.Value;
            Console.WriteLine($"Total cost of transportation will be: {totalCost:C}, your driver will be {driver.Name} on {vehicle.Name}");
        }

        request.SetTotalCost(totalCost);
        _driverRepository.ChangeAvailabilityToZeroById(driverId);
        _vehicleRepository.ChangeAvailabilityToZeroById(vehicleId);

        return request;
    }


}