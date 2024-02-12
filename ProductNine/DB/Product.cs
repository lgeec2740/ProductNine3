using System;
using System.Collections.Generic;
using ProductNine.DB;

namespace ProductNine;

public partial class Product
{
    public int Id { get; set; }

    public string? Article { get; set; }

    public string? Name { get; set; }

    public string? Unit { get; set; }

    public decimal? Price { get; set; }

    public decimal? MaxDiscount { get; set; }

    public int? IdManufacturer { get; set; }

    public int? IdProvider { get; set; }

    public int? IdCategory { get; set; }

    public decimal? CurrentDiscount { get; set; }

    public decimal? QuantityInStock { get; set; }

    public string? Description { get; set; }

    public byte[]? Image { get; set; }

    public virtual ICollection<Basket> Baskets { get; } = new List<Basket>();

    public virtual Category? IdCategoryNavigation { get; set; }

    public virtual Manufacturer? IdManufacturerNavigation { get; set; }

    public virtual Provider? IdProviderNavigation { get; set; }
}
