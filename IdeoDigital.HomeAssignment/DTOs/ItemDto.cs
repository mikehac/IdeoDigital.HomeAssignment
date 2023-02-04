namespace IdeoDigital.HomeAssignment.DTOs
{
    public class ItemDto
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }

        public string Description { get; set; } = null!;

        public int Quentity { get; set; }

        public decimal Rate { get; set; }

        //public virtual InvoiceDto Invoice { get; set; } = null!;
    }
}