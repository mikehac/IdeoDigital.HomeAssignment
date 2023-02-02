using AutoMapper;
using IdeoDigital.Entities;

namespace IdeoDigital.HomeAssignment.DTOs
{
    public class FullProfile : Profile
    {
        public FullProfile()
        {
            CreateMap<InvoiceDto, Invoice>()
                .ForMember(c => c.Customer, o => o.MapFrom(x => x.Customers))
                .ForMember(c => c.Supplier, o => o.MapFrom(x => x.Suppliers))
                .ForMember(c => c.Items, o => o.MapFrom(x => x.Items))
                .ReverseMap();

        }
    }
}
