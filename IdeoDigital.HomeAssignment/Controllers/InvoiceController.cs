using AutoMapper;
using IdeoDigital.Contracts;
using IdeoDigital.Entities;
using IdeoDigital.HomeAssignment.DTOs;
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

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
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

            try
            {
                //TODO: the crate method is better to return value of success/failer
                await _invoiceService.Create(invoice);
                return Ok();
            }
            catch (Exception ex)
            {
                //TODO:Logging the acceptions
                return StatusCode(StatusCodes.Status500InternalServerError, "The requst failed");
            }
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
    }
}
