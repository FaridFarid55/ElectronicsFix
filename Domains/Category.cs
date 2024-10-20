namespace Domains;

public partial class Category
{
    [Key]
    public int CategoryId { get; set; }
    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(100, ErrorMessage = "Category name can't be longer than 100 characters.")]

    public string CategoryName { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    [Display(Name = "Image Name")]
    public string? ImagePath { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? ParentCategoryId { get; set; }

    public bool? OnDelete { get; set; }

    public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual Category? ParentCategory { get; set; }
}
