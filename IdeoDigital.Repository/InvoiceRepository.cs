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
            _context.ChangeTracker.LazyLoadingEnabled = false;
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public async Task Create(Invoice invoice)
        {
            try
            {
                if (invoice.Items != null)
                {
                    await _context.Items.AddRangeAsync(invoice.Items);
                }
                Supplier? supplier = await _context.Suppliers.FirstOrDefaultAsync(x =>
                                    invoice.SupplierId != 0 ?
                                    x.Id == invoice.SupplierId :
                                    x.Name.ToLower().Equals(invoice.Supplier.Name.ToLower()));

                if (supplier == null)
                {
                    _context.Suppliers.Add(invoice.Supplier);
                }
                else
                {
                    //save the supplierId for using later on invoice aditing
                }
                Customer? customer = await _context.Customers.FirstOrDefaultAsync(x =>
                                    invoice.CustomerId != 0 ? x.Id == invoice.CustomerId :
                                    x.Name.ToLower().Equals(invoice.Customer.Name.ToLower()));
                if (customer == null)
                {
                    _context.Customers.Add(invoice.Customer);
                }

                _context.Invoices.Add(invoice);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var items = _context.Items.Where(x => x.InvoiceId == id);
                _context.Items.RemoveRange(items);
                var invoice = _context.Invoices.FirstOrDefault(x => x.Id == id);
                if (invoice != null)
                {
                    _context.Invoices.Remove(invoice);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Invoice[]> Get(int PageSize = 10)
        {
            IQueryable<Invoice> query = _context.Invoices
                .Include(x => x.Supplier)
                .Include(x => x.Customer)
                //.Include(x => x.IdNavigation)
                .Include(x => x.Status)
                .Take(PageSize);
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

        public async Task<Item[]> ItemsById(int id)
        {
            return await _context.Items.Where(x => x.InvoiceId == id).ToArrayAsync();
        }

        public void Update(Invoice invoice)
        {
            throw new NotImplementedException();
        }
    }
}
