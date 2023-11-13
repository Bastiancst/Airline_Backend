using Airline_DE.Models.Payment;
using Airline_DE.Models.Payment.DTOs;
using AutoMapper;

namespace Airline_DE.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {
            CreateMap<Payment, InvoiceDTO>().ReverseMap();
        }
    }
}
