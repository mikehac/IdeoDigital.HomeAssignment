using System;
using System.Collections.Generic;

namespace IdeoDigital.Entities;

public partial class Item
{
    public int Id { get; set; }

    public int InvoiceId { get; set; }

    public string Description { get; set; } = null!;

    public int Quentity { get; set; }

    public decimal Rate { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
}
