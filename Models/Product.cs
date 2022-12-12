using System;
using System.Collections.Generic;

namespace GeneralStore.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int QuantityInStock { get; set; }

    public double Price { get; set; }

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
