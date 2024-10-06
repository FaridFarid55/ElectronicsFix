namespace Bl;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int CustomerId { get; set; }

    public int OrderId { get; set; }

    public string? PaymentMethod { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
