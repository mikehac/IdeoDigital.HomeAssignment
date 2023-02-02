using IdeoDigital.Contracts;
using IdeoDigital.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeoDigital.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IdeoDigitalContext _context;

        public InvoiceRepository(IdeoDigitalContext context)
        {
            _context = context;
        }

        public void Create(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Invoice[]> Get()
        {
            IQueryable<Invoice> query = _context.Invoices
                .Include(x => x.Supplier)
                .Include(x => x.Customer)
                //.Include(x => x.IdNavigation)
                .Include(x => x.Status)
                .Take(20);
            return await query.ToArrayAsync();
        }

        public async Task<Invoice?> GetById(int id)
        {
            Invoice? invoice = await
                _context.Invoices
                .Include(x => x.Supplier)
                .Include(x => x.Customer)
                .Include(x => x.Items)
                .Include(x => x.Status)
                .FirstOrDefaultAsync(x => x.Id == id);
            return invoice;
        }

        public void Update(Invoice invoice)
        {
            throw new NotImplementedException();
        }
    }
}
