using AutoMapper;
using IdeoDigital.Contracts;
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
    }
}
