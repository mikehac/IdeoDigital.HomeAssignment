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

        public async Task<bool> Create(InvoiceDto invoice)
        {
            Invoice invoiceToSave = UpdateSubTotal(invoice);
            return await _invoiceRepository.Create(invoiceToSave);
        }

        private Invoice UpdateSubTotal(InvoiceDto invoice)
        {
            decimal subTotal = invoice.Items.Sum(x => x.Rate * x.Quentity);
            Invoice invoiceToSave = _mapper.Map<Invoice>(invoice);
            invoiceToSave.SubTotal = subTotal;
            return invoiceToSave;
        }

        public async Task<bool> Delete(int id)
        {
            var items = await _invoiceRepository.ItemsById(id);

            await _invoiceRepository.Delete(id);
            return true;
        }

        public async Task<InvoiceDto[]> Get(int PageSize = 20)
        {
            //TODO: calculate after discount and tax for Total
            return _mapper.Map<InvoiceDto[]>(await _invoiceRepository.Get(PageSize));
        }

        public async Task<InvoiceDto> GetById(int id)
        {
            return _mapper.Map<InvoiceDto>(await _invoiceRepository.GetById(id));
        }

        public async Task<bool> Update(InvoiceDto invoice)
        {
            Invoice invoiceToSave = UpdateSubTotal(invoice);
            return await _invoiceRepository.Update(invoiceToSave);
        }
    }
}
