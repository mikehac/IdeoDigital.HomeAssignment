using AutoMapper;
using IdeoDigital.Contracts;
using IdeoDigital.Entities;
using IdeoDigital.HomeAssignment.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace IdeoDigital.HomeAssignment.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public InvoiceService(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        public async Task Create(InvoiceDto invoice)
        {
            await _invoiceRepository.Create(_mapper.Map<Invoice>(invoice));
        }

        public async Task<InvoiceDto[]> Get(int PageSize = 10)
        {
            //TODO: calculate after discount and tax for Total
            return _mapper.Map<InvoiceDto[]>(await _invoiceRepository.Get(PageSize));
        }

        public async Task<InvoiceDto> GetById(int id)
        {
            return _mapper.Map<InvoiceDto>(await _invoiceRepository.GetById(id));
        }
    }
}
