using System;
using System.Collections.Generic;

namespace Aula24EfCore.Models;

public partial class TbRating
{
    public Guid Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string Comment { get; set; } = null!;

    public int Note { get; set; }

    public Guid UserId { get; set; }

    public Guid OrderId { get; set; }

    public virtual TbOrder Order { get; set; } = null!;

    public virtual TbUser User { get; set; } = null!;
}
