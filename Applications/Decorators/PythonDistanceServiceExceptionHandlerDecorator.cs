using transport_management_system.Domain.Exceptions;
using transport_management_system.Domain.Interfaces;

namespace transport_management_system.Applications.Decorators
{
    public class PythonDistanceServiceExceptionHandlerDecorator : IDistanceService
    {
        private readonly IDistanceService _inner;

        public PythonDistanceServiceExceptionHandlerDecorator(IDistanceService inner)
        {
            _inner = inner;
        }

        public decimal CalculateDistance(string from, string to)
        {
            try
            {
                return _inner.CalculateDistance(from, to);
            }
            catch (CityNotFoundException e)
            {
                Console.WriteLine($"Input error: {e.Message}");
                throw;
            }
            catch (CityNotInUkraineException e)
            {
                Console.WriteLine($"Invalid region: {e.Message}");
                throw;
            }
            catch (TimeoutException e)
            {
                Console.WriteLine($"Geocoding service unavailable: {e.Message}");
                throw;
            }

            throw new InvalidOperationException("Unreachable code");
        }
    }
}
