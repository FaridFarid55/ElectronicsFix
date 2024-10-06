using System;
using System.Collections.Generic;

namespace Domains;

public partial class ItemDetail
{
    public int ItemDetailsId { get; set; }

    public string Gpu { get; set; } = null!;

    public string HardDisk { get; set; } = null!;

    public string Processor { get; set; } = null!;

    public string RamSize { get; set; } = null!;

    public string ScreenResolution { get; set; } = null!;

    public string ScreenSize { get; set; } = null!;

    public string Weight { get; set; } = null!;

    public string OsName { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
