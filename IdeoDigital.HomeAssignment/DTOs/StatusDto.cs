namespace IdeoDigital.HomeAssignment.DTOs
{
    public class StatusDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<InvoiceDto> Invoices { get; } = new List<InvoiceDto>();
    }
}