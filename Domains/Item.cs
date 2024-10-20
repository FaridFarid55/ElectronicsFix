namespace Domains
{
    public partial class Item
    {
        [Key]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Item name is required.")]
        [StringLength(100, ErrorMessage = "Item name cannot be longer than 100 characters.")]
        public string ItemName { get; set; } = null!;

        [Required(ErrorMessage = "Sales price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Sales price must be greater than zero.")]
        public decimal SalesPrice { get; set; }

        [Required(ErrorMessage = "Purchase price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Purchase price must be greater than zero.")]
        public decimal PurchasePrice { get; set; }

        [Required(ErrorMessage = "Category Name is required.")]
        [Display(Name = "Category Name")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Item type is required.")]
        public string ItemType { get; set; } = null!;

        public int? ItemDetailsId { get; set; }

        [Required(ErrorMessage = "Image path is required.")]
        [Display(Name = "Image Name")]
        public string? ImagePath { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string? Description { get; set; }

        public bool? OnDelete { get; set; }

        [ValidateNever]
        public virtual Category Category { get; set; } = null!;
        public virtual ItemDetail? ItemDetails { get; set; }
        public virtual ICollection<ItemDiscount> ItemDiscounts { get; set; } = new List<ItemDiscount>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
