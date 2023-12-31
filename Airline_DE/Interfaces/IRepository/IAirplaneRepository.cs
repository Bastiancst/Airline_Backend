﻿using Airline_DE.Models.Airplane;
using Airline_DE.Models.Airport;

namespace Airline_DE.Interfaces.IRepository
{
    public interface IAirplaneRepository : IRepository<Airplane>
    {
        Task<Airplane> UpdateAsync(Airplane entity);
    }
}
