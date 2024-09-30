using System;
using System.Collections.Generic;

namespace ElectronicsFix.Models;

public partial class Consultation
{
    public int ConsultationId { get; set; }

    public int EngineerId { get; set; }

    public int CustomerId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }

    public string? IssueDescription { get; set; }

    public decimal ConsultationCost { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Engineer Engineer { get; set; } = null!;
}
