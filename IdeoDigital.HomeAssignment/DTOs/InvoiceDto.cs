namespace IdeoDigital.HomeAssignment.DTOs
{
    public class InvoiceDto
    {
        public int Id { get; set; }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public decimal? SubTotal { get; set; }

        public decimal? Tax { get; set; }

        public double? Discount { get; set; }

        public int StatusId { get; set; }
        public string InvoiceStatus { get; set; }

        public CustomerDto Customers { get; set; }

        public ICollection<ItemDto> Items { get; set; }

        public StatusDto Statuses { get; set; }

        public SupplierDto Suppliers { get; set; }
    }
}
