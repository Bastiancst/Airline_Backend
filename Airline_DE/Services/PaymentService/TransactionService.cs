using Airline_DE.Enums;
using Airline_DE.Interfaces;
using Airline_DE.Models.Payment.DTOs;
using Airline_DE.Settings;
using Airline_DE.Wrappers;
using RestSharp;
using System.Transactions;

namespace Airline_DE.Services.PaymentService
{
    public class TransactionService : ITransactionService
    {
        private readonly RestClient restClient;

        public TransactionService(IRestClientFactory restClientFactory)
        {
            this.restClient = restClientFactory.Create(DomainSettings.PaymentUrl);
        }

        public async Task<CreateTransactionResponseDTO> CreateTransaction(CreateTransactionRequestDTO request)
        {
            var restRequest = new RestRequest(
                "/api/payment",
                Method.Post);
            restRequest.AddJsonBody(request);
            restRequest.RequestFormat = DataFormat.Json;

            var response = await restClient.ExecuteAsync<ApiResponse<CreateTransactionResponseDTO>>(restRequest);


            return new CreateTransactionResponseDTO
            {
                Url = response.Data.Result.Url,
                Token = response.Data.Result.Token,
            };
        }
    }
}
