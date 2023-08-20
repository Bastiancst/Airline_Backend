using Airline_DE.Models.ClientAssingnment;

namespace Airline_DE.Interfaces.IRepository
{
    public interface IClientAssignmentRepository : IRepository<ClientAssignment>
    {
        Task<ClientAssignment> UpdateAsync (ClientAssignment entity);
    }
}
