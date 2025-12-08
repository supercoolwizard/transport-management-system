
using System.Runtime.InteropServices;
using Infrastructure.Repositories;
using transport_management_system.Domain.Entities;
using transport_management_system.Domain;

namespace Domain.Entities;

public class Request
{
    public int VehicleId { get; }
    public int DriverId { get; }
    public decimal Distance { get; }

    public decimal TotalCost { get; set; }

    public Request(int vehicleId, int driverId, decimal distance)
    {
        VehicleId = vehicleId;
        DriverId = driverId;
        Distance = distance;
        TotalCost = 0;
    }

    public void SetTotalCost(decimal cost)
    {
        TotalCost = cost;
    }

}

