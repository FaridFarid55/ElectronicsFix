namespace Domains;

public partial class Delivery
{
    [Key]
    public int DeliveryId { get; set; }

    public int OrderId { get; set; }

    public string DeliveryName { get; set; } = null!;

    public string DeliveryAddress { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? Status { get; set; }

    public virtual Order Order { get; set; } = null!;
}
