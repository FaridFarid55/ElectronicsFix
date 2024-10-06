namespace Bl;

public partial class Delivery
{
    public int DeliveryId { get; set; }

    public int OrderId { get; set; }

    public string? DeliveryName { get; set; }

    public string? DeliveryAddress { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }

    public virtual Order Order { get; set; } = null!;
}
