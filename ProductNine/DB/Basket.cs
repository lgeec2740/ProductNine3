using System;
using System.Collections.Generic;

namespace ProductNine.DB;

public partial class Basket
{
    public int IdProduct { get; set; }

    public int IdUser { get; set; }

    public decimal? Price { get; set; }

    public string? Count { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
