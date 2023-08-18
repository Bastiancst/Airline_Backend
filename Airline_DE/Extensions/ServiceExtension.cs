using Airline_DE.Interfaces.IRepository;
using Airline_DE.Repository;

namespace Airline_DE.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServiceExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAirplaneRepository, AirplaneRepository>();
        }
    }
}
