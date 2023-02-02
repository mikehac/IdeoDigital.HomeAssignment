using System;
using System.Collections.Generic;

namespace IdeoDigital.Entities;

public partial class Supplier
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public virtual ICollection<Invoice> Invoices { get; } = new List<Invoice>();
}
