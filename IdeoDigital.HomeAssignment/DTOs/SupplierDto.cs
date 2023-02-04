namespace IdeoDigital.HomeAssignment.DTOs
{
    public class SupplierDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Address { get; set; }

        public virtual ICollection<InvoiceDto> Invoices { get; } = new List<InvoiceDto>();
    }
}