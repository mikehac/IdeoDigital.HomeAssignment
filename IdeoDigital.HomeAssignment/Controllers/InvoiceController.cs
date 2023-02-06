using AutoMapper;
using IdeoDigital.Contracts;
using IdeoDigital.Entities;
using IdeoDigital.HomeAssignment.DTOs;
using IdeoDigital.HomeAssignment.DTOs.Requests;
using IdeoDigital.HomeAssignment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdeoDigital.HomeAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IMapper _mapper;

        public InvoiceController(IInvoiceService invoiceService, IMapper mapper)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
        }

        private IActionResult ValidateInvoice(CreateInvoiceRequest invoiceRequest)
        {
            if (invoiceRequest == null)
            {
                return BadRequest("Invoice can not be null");
            }
            if (invoiceRequest.Items == null || invoiceRequest.Items.Count == 0)
            {
                return BadRequest("Invoice must include at least one item");
            }
            if (string.IsNullOrEmpty(invoiceRequest.CustomerName))
            {
                return BadRequest("Invoice must include customer's details");
            }
            if (string.IsNullOrEmpty(invoiceRequest.SupplierName))
            {
                return BadRequest("Invoice must include supplier's details");
            }

            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var invoices = await _invoiceService.Get();
                if (invoices == null)
                {
                    return NotFound("There are no invoices yet");
                }
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                //TODO:Logging the acceptions
                return StatusCode(StatusCodes.Status500InternalServerError, "The requst failed");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var invoice = await _invoiceService.GetById(id);
                if (invoice == null)
                {
                    return NotFound($"Invoice with id {id} could not be found");
                }
                return Ok(invoice);
            }
            catch (Exception ex)
            {
                //TODO:Logging the acceptions
                return StatusCode(StatusCodes.Status500InternalServerError, "The requst failed");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInvoiceRequest invoiceRequest)
        {
            try
            {
                var result = ValidateInvoice(invoiceRequest);
                if (result is BadRequestObjectResult)
                    return result;

                InvoiceDto invoice = _mapper.Map<InvoiceDto>(invoiceRequest);
                if (await _invoiceService.Create(invoice))
                    return Created("/api/Invoice", invoice);
            }
            catch (Exception ex)
            {
                //TODO:Logging the acceptions
                return StatusCode(StatusCodes.Status500InternalServerError, "The requst failed");
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var invoice = await _invoiceService.GetById(id);
                if (invoice == null)
                    return NotFound($"Invoice with id {id} could not be found");
                if(await _invoiceService.Delete(id))
                    return Ok();
            }
            catch (Exception ex)
            {
                //TODO:Logging the acceptions
                return StatusCode(StatusCodes.Status500InternalServerError, "The requst failed");
            }
            return BadRequest("Failed to delete the invoice");
        }

        [HttpPut]
        public async Task<IActionResult> Put(CreateInvoiceRequest invoiceRequest)
        {
            try
            {
                var result = ValidateInvoice(invoiceRequest);
                if (result is BadRequestObjectResult)
                    return result;
                InvoiceDto invoice = _mapper.Map<InvoiceDto>(invoiceRequest);
                if (await _invoiceService.Update(invoice))
                    return StatusCode(StatusCodes.Status204NoContent, "The invice was successfully updated");
            }
            catch (Exception ex)
            {

                throw;
            }
            return BadRequest();
        }
    }
}
