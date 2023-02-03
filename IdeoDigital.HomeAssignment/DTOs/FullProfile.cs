using AutoMapper;
using IdeoDigital.Entities;

namespace IdeoDigital.HomeAssignment.DTOs
{
    public class FullProfile : Profile
    {
        public FullProfile()
        {
            CreateMap<Invoice, InvoiceDto>()
                .ForMember(c => c.Customers, o => o.MapFrom(x => x.Customer))
                .ForMember(c => c.CustomerName, o => o.MapFrom(x => x.Customer.Name))
                .ForMember(c => c.Suppliers, o => o.MapFrom(x => x.Supplier))
                .ForMember(c => c.SupplierName, o => o.MapFrom(x => x.Supplier.Name))
                .ForMember(c => c.InvoiceStatus, o => o.MapFrom(x => x.Status.Name))
                .ForMember(c => c.Items, o => o.MapFrom(x => x.Items))
                .ReverseMap();

        }
    }
}
