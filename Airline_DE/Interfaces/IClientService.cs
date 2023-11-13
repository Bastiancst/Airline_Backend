using Airline_DE.Models.Employee.DTOs;
using Airline_DE.Models.Employee;
using Airline_DE.Wrappers;
using Airline_DE.Models.Client;
using Airline_DE.Models.Client.DTOs;
using Airline_DE.Models.Payment.DTOs;

namespace Airline_DE.Interfaces
{
    public interface IClientService
    {
        Task<ApiResponse<Client>> CreateAsync(CreateClientDTO client);
        Task<ApiResponse<Client>> GetByIdAsync(Guid clientId);
        Task<ApiResponse<IEnumerable<Client>>> GetAllAsync();
        Task<ApiResponse<Client>> UpdateAsync(UpdateClientDTO client, Guid id);
        Task<ApiResponse<Guid>> DeleteAsync(Guid client);
        Task<ApiResponse<GetEmployeebyClientDTO>> GetEmployeebyClient(Guid clientId);
        Task<ApiResponse<IEnumerable<InvoiceDTO>>> GetInvoicesClient(Guid clientId);
    }
}
