using System;
using System.Collections.Generic;

namespace Aula24EfCore.Models;

public partial class TbOrder
{
    public Guid Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string PaymentType { get; set; } = null!;

    public decimal Total { get; set; }

    public Guid UserId { get; set; }

    public virtual ICollection<TbOrderProduct> TbOrderProducts { get; set; } = new List<TbOrderProduct>();

    public virtual ICollection<TbRating> TbRatings { get; set; } = new List<TbRating>();

    public virtual TbUser User { get; set; } = null!;
}
