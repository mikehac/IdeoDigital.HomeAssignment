﻿using IdeoDigital.Contracts;
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

        public async Task<bool> Create(Invoice invoice)
        {
            try
            {
                int supplierId;
                int customerId;
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
                    await _context.SaveChangesAsync();
                    supplierId = invoice.Supplier.Id;
                }
                else
                {
                    supplierId = supplier.Id;
                }
                
                Customer? customer = await _context.Customers.FirstOrDefaultAsync(x =>
                                    invoice.CustomerId != 0 ? x.Id == invoice.CustomerId :
                                    x.Name.ToLower().Equals(invoice.Customer.Name.ToLower()));
                if (customer == null)
                {
                    _context.Customers.Add(invoice.Customer);
                    await _context.SaveChangesAsync();
                    customerId = invoice.Customer.Id;
                }
                else
                {
                    customerId = customer.Id;
                }
                invoice.SupplierId = supplierId;
                invoice.CustomerId = customerId;
                _context.Invoices.Add(invoice);
                if (await _context.SaveChangesAsync() > 0)
                    return true;
                return false;
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

        public async Task<Invoice[]> Get(int PageSize = 20)
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

        public async Task Update(Invoice invoice)
        {
            try
            {
                Invoice? oldInvoice = _context.Invoices.FirstOrDefault(x => x.Id == invoice.Id);
                if (oldInvoice == null)
                {
                    return;
                }

                List<Item> oldItems = _context.Items.Where(x => x.InvoiceId == invoice.Id).ToList();
                if(oldItems != null) 
                {
                    return;
                }
                
                _context.Items.RemoveRange(oldItems);
                await _context.SaveChangesAsync();
                await _context.Items.AddRangeAsync(invoice.Items);
                await _context.SaveChangesAsync();

                var supplier = _context.Suppliers.FirstOrDefault(x => 
                                                            invoice.Supplier.Id != 0 ? 
                                                                    x.Id == invoice.Supplier.Id : 
                                                                    x.Name.ToLower().Equals(invoice.Supplier.Name.ToLower()));
                if (supplier != null)
                {
                    //meaning the supplier was changed, replace the old supplierId with the new one
                    _context.Suppliers.Add(invoice.Supplier);
                    await _context.SaveChangesAsync();
                    invoice.SupplierId = invoice.Supplier.Id;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    supplier.Name = invoice.Supplier.Name;
                    supplier.Address = invoice.Supplier.Address;
                    await _context.SaveChangesAsync();
                }

                var customer = _context.Customers.FirstOrDefault(x =>
                                                            invoice.Customer.Id != 0 ?
                                                                x.Id == invoice.Customer.Id :
                                                                x.Name.ToLower().Equals(invoice.Customer.Name.ToLower()));
                if (customer != null)
                {
                    //meaning the customer was changed, replace the old customerId with the new one
                    _context.Customers.Add(invoice.Customer);
                    await _context.SaveChangesAsync();
                    invoice.CustomerId = invoice.Customer.Id;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    customer.Name = invoice.Customer.Name;
                    customer.Address = invoice.Customer.Address;
                    customer.ShippingAddress = invoice.Customer.ShippingAddress;
                    await _context.SaveChangesAsync();
                }

                var oldinvoice = _context.Invoices.FirstOrDefault(x => x.Id == invoice.Id);
                if(oldinvoice != null)
                {
                    oldinvoice.CustomerId = invoice.CustomerId;
                    oldinvoice.SupplierId = invoice.SupplierId;
                    oldinvoice.StatusId = invoice.StatusId;
                    oldinvoice.Date = invoice.Date;
                    oldinvoice.DueDate = invoice.DueDate;
                    oldinvoice.Tax = invoice.Tax;
                    oldinvoice.Discount = invoice.Discount;
                    decimal subTotal = invoice.Items.Sum(x => x.Rate * x.Quentity);
                    oldinvoice.SubTotal = subTotal;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
