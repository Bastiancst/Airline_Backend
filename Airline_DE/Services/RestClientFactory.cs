using Airline_DE.Interfaces;
using RestSharp;

namespace Airline_DE.Services
{
    public class RestClientFactory : IRestClientFactory
    {
        public RestClient Create(string url)
        {
            return new RestClient(url);
        }
    }
}
