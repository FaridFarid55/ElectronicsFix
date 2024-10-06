using System;
using System.Collections.Generic;

namespace Domains;

public partial class ItemDiscount
{
    public int ItemDiscountId { get; set; }

    public int ItemId { get; set; }

    public decimal? DiscountPercent { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual Item Item { get; set; } = null!;
}
