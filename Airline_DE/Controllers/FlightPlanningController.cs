using Airline_DE.Interfaces.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Airline_DE.Controllers
{
    public class FlightPlanningController : ControllerBase
    {
        private readonly IFlightPlanningRepository _flightPlanningRepository;
        private readonly IAirportRepository _airportRepository;


        //[HttpPost]
        //public async Task<IActionResult> GetFlightPlanning()
        //{
            
        //}
    } 
}
