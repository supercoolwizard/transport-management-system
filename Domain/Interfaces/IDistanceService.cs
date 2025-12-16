namespace transport_management_system.Domain.Interfaces;

public interface IDistanceService
{
    decimal CalculateDistance(string from, string to);
}