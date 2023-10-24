using Airline_DE.Models.Client.DTOs;
using Airline_DE.Models.Client;
using Airline_DE.Wrappers;
using Airline_DE.Models.Assignment.DTOs;
using Airline_DE.Models.Assignment;

namespace Airline_DE.Interfaces
{
    public interface IAssignmentService
    {
        Task<ApiResponse<Guid>> CreateAsync(CreateAssignmentDTO request, Guid clientId);
        Task<ApiResponse<IEnumerable<Assignment>>> GetAllAsync(Guid clienId);
        Task<ApiResponse<Assignment>> GetByIdAsync(Guid id);

        //Task<ApiResponse<Client>> GetByIdAsync(Guid clientId);
        //Task<ApiResponse<IEnumerable<Client>>> GetAllAsync();
        //Task<ApiResponse<Client>> UpdateAsync(UpdateClientDTO client, Guid id);
        //Task<ApiResponse<Guid>> DeleteAsync(Guid client);
    }
}
