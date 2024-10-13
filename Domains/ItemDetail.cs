using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domains
{
    public partial class ItemDetail
    {
        public int ItemDetailsId { get; set; }

        [Required(ErrorMessage = "GPU is required.")]
        public string Gpu { get; set; } = null!;

        [Required(ErrorMessage = "Hard disk is required.")]
        public string HardDisk { get; set; } = null!;

        [Required(ErrorMessage = "Processor is required.")]
        public string Processor { get; set; } = null!;

        [Required(ErrorMessage = "RAM size is required.")]
        public string RamSize { get; set; } = null!;

        [Required(ErrorMessage = "Screen resolution is required.")]
        public string ScreenResolution { get; set; } = null!;

        [Required(ErrorMessage = "Screen size is required.")]
        public string ScreenSize { get; set; } = null!;

        [Required(ErrorMessage = "Weight is required.")]
        public string Weight { get; set; } = null!;

        [Required(ErrorMessage = "Operating system name is required.")]
        public string OsName { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
