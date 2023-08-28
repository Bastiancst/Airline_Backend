using Airline_DE.Models.Airplane;
using Airline_DE.Models.Payment;

namespace Airline_DE.Interfaces.IRepository
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<Payment> UpdateAsync(Payment entity);
    }
}

