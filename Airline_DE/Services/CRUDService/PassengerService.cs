using Airline_DE.Interfaces;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Passenger;
using Airline_DE.Models.Passenger.DTOs;
using Airline_DE.Wrappers;

namespace Airline_DE.Services.CRUDService
{
    public class PassengerService : IPassengerService
    {
        private readonly IPassengerRepository _passengerRepository;

        public PassengerService(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        public async Task<ApiResponse<Guid>> CreateAsync(List<CreatePassengerDTO> request)
        {
            Guid clientId = Guid.Empty;

            foreach (var dto in request)
            {
                var passenger = new Passenger()
                {
                    Id = Guid.NewGuid(),
                    ClientId = dto.ClientId,
                    FlightPlanningId = dto.FlightPlanningId,
                    PaymentId = Guid.NewGuid(),
                    Name = dto.Name,
                    LastName = dto.LastName,
                    Age = dto.Age,
                    Address = dto.Address,
                    PhoneNumber = dto.PhoneNumber,
                    Email = dto.Email,
                    SeatNumber = dto.SeatNumber,
                    isCopyDocumentEmail = dto.isCopyDocumentEmail,
                    IdentityDocument = dto.IdentityDocument,
                    
                };

                clientId = passenger.ClientId;

                await _passengerRepository.CreateAsync(passenger);
            }


            return new ApiResponse<Guid>(clientId);
        }

        public async Task<ApiResponse<IEnumerable<Passenger>>> GetAllAsync(Guid clienId, Guid flightPlanningId)
        {

            var passengers = await _passengerRepository.GetAllAsync(x => x.ClientId == clienId && x.FlightPlanningId == flightPlanningId);

            return new ApiResponse<IEnumerable<Passenger>>(passengers);
        }

    }
}
