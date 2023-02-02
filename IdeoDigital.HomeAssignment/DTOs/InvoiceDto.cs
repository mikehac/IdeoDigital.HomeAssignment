using IdeoDigital.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdeoDigital.HomeAssignment.DTOs
{
    public class InvoiceDto
    {
        public int Id { get; set; }

        public int SupplierId { get; set; }

        public int CustomerId { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public decimal? SubTotal { get; set; }

        public decimal? Tax { get; set; }

        public double? Discount { get; set; }

        public int ItemId { get; set; }

        public int StatusId { get; set; }

        public Customer Customers { get; set; }

        public Invoice Invoices { get; set; }

        public ICollection<Item> Items { get; set; }

        public Status Statuses { get; set; }

        public Supplier Suppliers { get; set; }
    }
}
