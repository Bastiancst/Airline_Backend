using RestSharp;

namespace Airline_DE.Interfaces
{
    public interface IRestClientFactory
    {
        RestClient Create(string url);
    }
}
