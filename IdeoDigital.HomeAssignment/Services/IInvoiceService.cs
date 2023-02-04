using IdeoDigital.HomeAssignment.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeoDigital.HomeAssignment.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceDto[]> Get(int PageSize = 10);
        Task<InvoiceDto> GetById(int id);
        Task Create(InvoiceDto invoice);
        Task<bool> Delete(int id);
    }
}
