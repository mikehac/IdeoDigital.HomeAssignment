using AutoMapper;
using IdeoDigital.Entities;
using IdeoDigital.HomeAssignment.DTOs.Requests;

namespace IdeoDigital.HomeAssignment.DTOs
{
    public class FullProfile : Profile
    {
        public FullProfile()
        {
            CreateMap<Invoice, InvoiceDto>()
                .ForMember(c => c.Customers, o => o.MapFrom(x => x.Customer))
                .ForMember(c => c.CustomerName, o => o.MapFrom(x => x.Customer.Name))
                .ForMember(c => c.CustomerAddress, o => o.MapFrom(x => x.Customer.Address))
                .ForMember(c => c.Suppliers, o => o.MapFrom(x => x.Supplier))
                .ForMember(c => c.SupplierName, o => o.MapFrom(x => x.Supplier.Name))
                .ForMember(c => c.SupplierAddress, o => o.MapFrom(x => x.Supplier.Address))
                .ForMember(c => c.InvoiceStatus, o => o.MapFrom(x => x.Status.Name))
                .ForMember(c => c.Items, o => o.MapFrom(x => x.Items))
                .ReverseMap();

            CreateMap<Customer, CustomerDto>()
                .ForMember(c => c.Invoices, o => o.Ignore())
                .ReverseMap();

            CreateMap<Supplier, SupplierDto>()
                .ForMember(c => c.Invoices, o => o.Ignore())
                .ReverseMap();

            CreateMap<Item, ItemDto>()
                .ReverseMap();

            CreateMap<Status, StatusDto>()
                .ReverseMap();

            CreateMap<CreateInvoiceRequest, InvoiceDto>();
        }
    }
}
