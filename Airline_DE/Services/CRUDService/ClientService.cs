using Airline_DE.Interfaces;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Client;
using Airline_DE.Models.Client.DTOs;
using Airline_DE.Models.Employee;
using Airline_DE.Models.Payment.DTOs;
using Airline_DE.Repository;
using Airline_DE.Wrappers;
using AutoMapper;
using Azure.Core;

namespace Airline_DE.Services.CRUDService
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IEmployeeRepository employeeRepository, IPaymentRepository paymentRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _employeeRepository = employeeRepository;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<Client>> CreateAsync(CreateClientDTO client)
        {
            var newClient = new Client
            {
                Id = Guid.Parse(client.Id),
                Addres = client.Addres,
                Email = client.Email,
                Name = client.Name,
                PhoneNumber = client.PhoneNumber,
                EmployeeId = Guid.NewGuid(),
                PaimentId = Guid.NewGuid()
            };

            await _clientRepository.CreateAsync(newClient);

            return new ApiResponse<Client>(newClient);
        }

        public async Task<ApiResponse<Guid>> DeleteAsync(Guid id)
        {
            var result = await _clientRepository.GetAsync(u => u.Id == id);

            if (result == null)
            {
                return new ApiResponse<Guid>("Client not found");
            }

            await _clientRepository.RemoveAsync(result);

            return new ApiResponse<Guid>(result.Id);
        }

        public async Task<ApiResponse<IEnumerable<Client>>> GetAllAsync()
        {
            var result = await _clientRepository.GetAllAsync();

            return new ApiResponse<IEnumerable<Client>>(result);
        }

        public async Task<ApiResponse<Client>> GetByIdAsync(Guid clientId)
        {
            var result = await _clientRepository.GetAsync(u => u.Id == clientId);

            if (result == null)
            {
                return new ApiResponse<Client>("Client not found");
            }

            return new ApiResponse<Client>(result);
        }

        public async Task<ApiResponse<GetEmployeebyClientDTO>> GetEmployeebyClient(Guid clientId)
        {
            var client = await _clientRepository.GetAsync(x => x.Id == clientId);
            if (client == null)
            {
                return new ApiResponse<GetEmployeebyClientDTO>($"Client not found with Id: {clientId}");
            }

            var employee = await _employeeRepository.GetAsync(x => x.Id == client.EmployeeId);


            return new ApiResponse<GetEmployeebyClientDTO>(new GetEmployeebyClientDTO
            {
                Email = employee.Email,
                LastName = employee.LastName,
                Name = employee.Name,
            });
        }

        public async Task<ApiResponse<IEnumerable<InvoiceDTO>>> GetInvoicesClient(Guid clientId)
        {
            var payments = await _paymentRepository.GetAllAsync(x => x.ClientId == clientId);

            var result = _mapper.Map<IEnumerable<InvoiceDTO>>(payments);

            return new ApiResponse<IEnumerable<InvoiceDTO>>(result);
        }

        public async Task<ApiResponse<Client>> UpdateAsync(UpdateClientDTO request, Guid id)
        {
            try
            {
                var result = await _clientRepository.GetAsync(u => u.Id == id);

                if (request == null)
                {
                    return new ApiResponse<Client>("User not found");
                }

                result.Name = request.Name;
                result.PhoneNumber = request.PhoneNumber;
                result.Addres = request.Addres;
                result.Email = request.Email;

                await _clientRepository.UpdateAsync(result);

                return new ApiResponse<Client>(result);
            }
            catch (Exception ex)
            {

                return new ApiResponse<Client>("efe");
            }
        }
    }
}
