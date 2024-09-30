using System;
using System.Collections.Generic;

namespace ElectronicsFix.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public decimal SalesPrice { get; set; }

    public decimal PurchasePrice { get; set; }

    public int CategoryId { get; set; }

    public string? ItemType { get; set; }

    public int? ItemDetailsId { get; set; }

    public string? ImagePath { get; set; }

    public string? Description { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ItemDetail? ItemDetails { get; set; }

    public virtual ICollection<ItemDiscount> ItemDiscounts { get; set; } = new List<ItemDiscount>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
