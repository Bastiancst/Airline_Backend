using Airline_DE.Interfaces;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Assignment;
using Airline_DE.Models.Assignment.DTOs;
using Airline_DE.Models.Receiver;
using Airline_DE.Wrappers;

namespace Airline_DE.Services.CRUDService
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignment;
        private readonly IReceiverRepository _receiver;

        public AssignmentService(IAssignmentRepository assignment, IReceiverRepository receiver)
        {
            _assignment = assignment;
            _receiver = receiver;
        }

        public async Task<ApiResponse<Guid>> CreateAsync(CreateAssignmentDTO request, Guid clientId)
        {
            var assignment = new Assignment
            {
                Id = Guid.NewGuid(),
                ClientId = clientId,
                ReceiverId = Guid.NewGuid(),
                Height = request.Height,
                Weight = request.Weight,
                Wide = request.Wide,
                Lenght = request.Lenght,
                Origin = request.Origin,
                Destination = request.Destination,
                IsCopyDocumentEmail = request.IsCopyDocumentEmail,
            };

            var receiver = new Receiver
            {
                Id = assignment.ReceiverId,
                Name = request.Receiver.Name,
                LastName = request.Receiver.LastName,
                Addres = request.Receiver.Addres,
                PhoneNumber = request.Receiver.PhoneNumber,
                Email = request.Receiver.Email,
            };

            await _assignment.CreateAsync(assignment);
            await _receiver.CreateAsync(receiver);

            return new ApiResponse<Guid>(clientId);
        }
    }
}
