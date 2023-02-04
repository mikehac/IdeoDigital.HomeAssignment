namespace IdeoDigital.HomeAssignment.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string? ShippingAddress { get; set; }
        public virtual ICollection<InvoiceDto> Invoices { get; } = new List<InvoiceDto>();
    }
}
