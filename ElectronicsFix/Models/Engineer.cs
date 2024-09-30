using System;
using System.Collections.Generic;

namespace ElectronicsFix.Models;

public partial class Engineer
{
    public int EngineerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string ConfirmPassword { get; set; } = null!;

    public virtual ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();
}
