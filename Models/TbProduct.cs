using System;
using System.Collections.Generic;

namespace Aula24EfCore.Models;

public partial class TbProduct
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Value { get; set; }

    public int? StockQuantity { get; set; }

    public virtual ICollection<TbOrderProduct> TbOrderProducts { get; set; } = new List<TbOrderProduct>();
}
