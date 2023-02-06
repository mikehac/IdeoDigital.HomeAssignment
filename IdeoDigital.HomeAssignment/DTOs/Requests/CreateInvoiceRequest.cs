namespace IdeoDigital.HomeAssignment.DTOs.Requests
{
    public class CreateInvoiceRequest
    {
        public int? Id { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public decimal? SubTotal { get; set; }

        public decimal? Tax { get; set; }

        public double? Discount { get; set; }

        public int StatusId { get; set; }
        public string InvoiceStatus { get; set; }
        public ICollection<ItemDto> Items { get; set; }

    }
}
