namespace Bl;

public partial class ItemDetail
{
    public int ItemDetailsId { get; set; }

    public string? Gpu { get; set; }

    public string? HardDisk { get; set; }

    public string? Processor { get; set; }

    public string? RamSize { get; set; }

    public string? ScreenResolution { get; set; }

    public string? ScreenSize { get; set; }

    public string? Weight { get; set; }

    public string? OsName { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
