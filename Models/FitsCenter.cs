using System;
using System.ComponentModel.DataAnnotations;

namespace ATI_IEC.Models
{
    public class FitsCenter
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "FITS Center")]
        public string CenterName { get; set; } = null!;

        [Required]
        [Display(Name = "Launched Date")]
        public DateTime LaunchedDate { get; set; }

        [Required]
        public string Status { get; set; } = "Active"; // Active / Inactive

        [Required]
        public string Address { get; set; } = null!;

        [Display(Name = "Center In-Charge")]
        public string? InCharge { get; set; }

        [EmailAddress]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }
    }
}
