using Airline_DE.Models.Airplane;
using Airline_DE.Models.Airport;
using Airline_DE.Models.Assignment;
using Airline_DE.Models.Client;
using Airline_DE.Models.ClientAssingnment;
using Airline_DE.Models.Employee;
using Airline_DE.Models.FlightPlanning;
using Airline_DE.Models.Office;
using Airline_DE.Models.Passenger;
using Airline_DE.Models.PassengerAsignment;
using Airline_DE.Models.Payment;
using Airline_DE.Models.PlanningCrew;
using Airline_DE.Models.Receiver;
using Airline_DE.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Airline_DE.DbContext
{
    public class Context : IdentityDbContext<User>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Airplane> Airplanes { get ; set; } 
        public DbSet<Airport> Airports { get ; set; } 
        public DbSet<Assignment> Assignments { get ; set; } 
        public DbSet<Client> Clients { get ; set; } 
        public DbSet<ClientAssignment> ClientAssignments { get ; set; } 
        public DbSet<Employee> Employees { get ; set; } 
        public DbSet<FlightPlanning> FlightPlannings { get ; set; } 
        public DbSet<Office> Offices { get ; set; } 
        public DbSet<Passenger> Passengers { get ; set; } 
        public DbSet<PassengerAsignment> PassengerAsignments { get ; set; } 
        public DbSet<Payment> Payments { get ; set; } 
        public DbSet<PlanningCrew> PlanningCrews { get ; set; } 
        public DbSet<Receiver> Receivers { get ; set; } 
    }
}
