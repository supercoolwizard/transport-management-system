namespace transport_management_system.Applications.DTOs;

public class RequestResultDto
{
    public decimal DistanceKm { get; init; }
    public decimal TotalCost { get; init; }
    
    public string DriverName { get; init; } = string.Empty;
    public string VehicleName { get; init; } = string.Empty;
}