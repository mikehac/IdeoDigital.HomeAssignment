using AutoMapper;
using IdeoDigital.Contracts;
using IdeoDigital.Entities;
using IdeoDigital.HomeAssignment.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdeoDigital.HomeAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public InvoiceController(IInvoiceRepository invoiceRepository,
            IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var invoices = await _invoiceRepository.Get();
            if(invoices == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<InvoiceDto[]>(invoices));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var invoice = await _invoiceRepository.GetById(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<InvoiceDto>(invoice));
        }

        [HttpPost]
        public async Task<IActionResult> Create(InvoiceDto invoice)
        {
            if(invoice == null)
            {
                return BadRequest("Invoice can not be null");
            }
            if (invoice.Items == null || invoice.Items.Count == 0)
            {
                return BadRequest("Invoice must include at least one item");
            }
            if (invoice.Customers == null && invoice.CustomerId == 0)
            {
                return BadRequest("Invoice must include customer's details");
            }
            if (invoice.Suppliers == null && invoice.SupplierId == 0)
            {
                return BadRequest("Invoice must include supplier's details");
            }

            //TODO: the crate method is better to return value of success/failer
            await _invoiceRepository.Create(_mapper.Map<Invoice>(invoice));
            return Ok();
        }
    }
}
