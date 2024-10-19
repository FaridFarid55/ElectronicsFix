// Ignore Spelling: FacebookLink Facebook Googol Instagram

namespace Domains
{
    // Form Farid Farid
    public class TbSetting
    {
        public int ID { get; set; }

        [MaxLength(400)]
        [ValidateNever]
        [Required(ErrorMessage = "Please Enter Image")]
        public string? Logo { get; set; }

        [MaxLength(2000)]
        [Required(ErrorMessage = "Please Enter Description")]
        public string? Description { get; set; }

        [MaxLength(400)]
        [Required(ErrorMessage = "Please Enter Copyright")]
        public string? Copyright { get; set; }

        [MaxLength(400)]
        [Required(ErrorMessage = "Please Enter Mail")]
        public string? Mail { get; set; }

        [MaxLength(400)]
        [Required(ErrorMessage = "Please Enter Phone")]
        public string? Phone { get; set; }

        [MaxLength(400)]
        [Required(ErrorMessage = "Please Enter Facebook Link")]
        public string? Facebook_Link { get; set; }

        [MaxLength(400)]
        [Required(ErrorMessage = "Please Enter Googol Link")]
        public string? Googol_Link { get; set; }

        [MaxLength(400)]
        [Required(ErrorMessage = "Please Enter TWitter Link")]
        public string? Twitter_Link { get; set; }

        [MaxLength(400)]
        [Required(ErrorMessage = "Please Enter Instagram Link")]
        public string? Instagram_Link { get; set; }

        [MaxLength(400)]
        [Required(ErrorMessage = "Please Enter LinkedIN Link")]
        public string? LinkedIn_Link { get; set; }
    }
}
