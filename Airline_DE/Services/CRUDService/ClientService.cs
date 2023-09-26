using Airline_DE.Interfaces;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Client;
using Airline_DE.Models.Client.DTOs;
using Airline_DE.Models.Employee;
using Airline_DE.Repository;
using Airline_DE.Wrappers;
using Azure.Core;

namespace Airline_DE.Services.CRUDService
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clienRepository;

        public ClientService(IClientRepository clienRepository)
        {
            _clienRepository = clienRepository;
        }

        public async Task<ApiResponse<Client>> CreateAsync(CreateClientDTO client)
        {
            var newClient = new Client
            {
                Id = Guid.NewGuid(),
                Addres = client.Addres,
                Email = client.Email,
                Name = client.Name,
                PhoneNumber = client.PhoneNumber,
                EmployeeId = Guid.NewGuid(),
                PaimentId = Guid.NewGuid()
            };

            await _clienRepository.CreateAsync(newClient);

            return new ApiResponse<Client>(newClient);
        }

        public async Task<ApiResponse<Guid>> DeleteAsync(Guid id)
        {
            var result = await _clienRepository.GetAsync(u => u.Id == id);

            await _clienRepository.RemoveAsync(result);

            return new ApiResponse<Guid>(result.Id);
        }

        public async Task<ApiResponse<IEnumerable<Client>>> GetAllAsync()
        {
            var result = await _clienRepository.GetAllAsync();

            return new ApiResponse<IEnumerable<Client>>(result);
        }

        public async Task<ApiResponse<Client>> GetByIdAsync(Guid clientId)
        {
            var result = await _clienRepository.GetAsync(u => u.Id == clientId);

            return new ApiResponse<Client>(result);
        }

        public async Task<ApiResponse<Client>> UpdateAsync(UpdateClientDTO request, Guid id)
        {
            var result = await _clienRepository.GetAsync(u => u.Id == id);

            if (request == null)
            {
                return new ApiResponse<Client>("User not found");
            }

            result.Name = request.Name;
            result.PhoneNumber = request.PhoneNumber;  
            result.Addres = request.Addres;
            result.Email = request.Email;

            await _clienRepository.RemoveAsync(result);

            return new ApiResponse<Client>(result);
        }
    }
}
