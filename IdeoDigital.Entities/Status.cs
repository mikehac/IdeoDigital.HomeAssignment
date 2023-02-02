using System;
using System.Collections.Generic;

namespace IdeoDigital.Entities;

public partial class Status
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; } = new List<Invoice>();
}
