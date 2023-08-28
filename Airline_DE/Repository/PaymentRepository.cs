using Airline_DE.DbContext;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Models.Airplane;
using Airline_DE.Models.Payment;

namespace Airline_DE.Repository
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly Context _context;

        public PaymentRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Payment> UpdateAsync(Payment entity)
        {
            _context.Payments.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
