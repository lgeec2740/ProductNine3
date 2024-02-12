using System;
using System.Collections.Generic;

namespace ProductNine.DB;

public partial class User
{
    public int Id { get; set; }

    public string? Surname { get; set; }

    public string? NameAndPatronymicName { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public int? IdRole { get; set; }

    public virtual ICollection<Basket> Baskets { get; } = new List<Basket>();

    public virtual Role? IdRoleNavigation { get; set; }
}
