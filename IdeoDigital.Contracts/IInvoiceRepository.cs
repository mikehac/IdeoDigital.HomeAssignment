using IdeoDigital.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeoDigital.Contracts
{
    public interface IInvoiceRepository
    {
        Task<Invoice[]> Get(int PageSize = 20);
        Task<Invoice?> GetById(int id);
        Task<bool> Create(Invoice invoice);
        Task Update(Invoice invoice);
        Task Delete(int id);
        Task<Item[]> ItemsById(int id);
    }
}
