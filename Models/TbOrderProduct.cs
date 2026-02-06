using System;
using System.Collections.Generic;

namespace Aula24EfCore.Models;

public partial class TbOrderProduct
{
    public int Quantity { get; set; }

    public Guid ProductId { get; set; }

    public Guid OrderId { get; set; }

    public virtual TbOrder Order { get; set; } = null!;

    public virtual TbProduct Product { get; set; } = null!;
}
