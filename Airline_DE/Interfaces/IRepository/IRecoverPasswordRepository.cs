using Airline_DE.Models.Airplane;
using Airline_DE.Models.Airport;
using Airline_DE.Models.User;

namespace Airline_DE.Interfaces.IRepository
{
    public interface IRecoverPasswordRepository : IRepository<RecoverPassword>
    {
        Task<RecoverPassword> UpdateAsync(RecoverPassword entity);
    }
}
