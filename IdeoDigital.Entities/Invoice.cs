using System;
using System.Collections.Generic;

namespace IdeoDigital.Entities;

public partial class Invoice
{
    public int Id { get; set; }

    public int SupplierId { get; set; }

    public int CustomerId { get; set; }

    public DateTime Date { get; set; }

    public DateTime DueDate { get; set; }

    public decimal? SubTotal { get; set; }

    public decimal? Tax { get; set; }

    public double? Discount { get; set; }

    public int StatusId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Item> Items { get; } = new List<Item>();

    public virtual Status Status { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
