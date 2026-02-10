using System;
using System.Collections.Generic;

namespace Aula24EfCore.Models;

public partial class TbUser
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<TbOrder> TbOrders { get; set; } = new List<TbOrder>();

    public virtual ICollection<TbRating> TbRatings { get; set; } = new List<TbRating>();
}
