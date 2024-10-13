using System;
using System.ComponentModel.DataAnnotations;

namespace Domains
{
    public partial class ItemDiscount
    {
        public int ItemDiscountId { get; set; }

        [Required(ErrorMessage = "Item ID is required.")]
        public int ItemId { get; set; }

        [Range(0, 100, ErrorMessage = "Discount percent must be between 0 and 100.")]
        public decimal? DiscountPercent { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }

        public virtual Item Item { get; set; } = null!;
    }
}
