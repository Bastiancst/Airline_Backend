﻿using Airline_DE.Interfaces;
using Airline_DE.Interfaces.IRepository;
using Airline_DE.Repository;
using Airline_DE.Services.AccountServices;
using Airline_DE.Services.ComunicationService;
using Airline_DE.Services.CRUDService;
using Airline_DE.Services.EmailService;

namespace Airline_DE.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServiceExtension(this IServiceCollection services, IConfiguration configuration)
        {
            #region Services
            services.AddTransient<IEmailServices, EmailServices>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IAccountServices, AccountServices>();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IAssignmentService, AssignmentService>();
            services.AddTransient<IChatService, ChatService>();
            #endregion

            #region Repositories
            services.AddTransient<IAirplaneRepository, AirplaneRepository>();
            services.AddTransient<IAirportRepository, AirportRepository>();
            services.AddTransient<IAssignmentRepository, AssignmentRepository>();
            services.AddTransient<IClientAssignmentRepository, ClientAssignmentRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IFlightPlanningRepository, FlightPlanningRepository>();
            services.AddTransient<IOfficeRepository, OfficeRepository>();
            services.AddTransient<IPassengerRepository, PassengerRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IPlanningCrewRepository, PlanningCrewRepository>();
            services.AddTransient<IReceiverRepository, ReceiverRepository>();
            services.AddTransient<IRecoverPasswordRepository, RecoverPasswordRepository>();
            services.AddTransient<IPassengerService, PassengerService>();
            services.AddTransient<IChatMessageRepository, ChatMessageRepository>();

            #endregion

        }
    }
}
