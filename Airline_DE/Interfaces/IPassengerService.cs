using Airline_DE.Models.Assignment.DTOs;
using Airline_DE.Models.Assignment;
using Airline_DE.Wrappers;
using Airline_DE.Models.Passenger.DTOs;
using Airline_DE.Models.Passenger;

namespace Airline_DE.Interfaces
{
    public interface IPassengerService
    {
        Task<ApiResponse<Guid>> CreateAsync(List<CreatePassengerDTO> request);
        Task<ApiResponse<IEnumerable<Passenger>>> GetAllAsync(Guid clienId, Guid flightPlanningId);
    }
}
