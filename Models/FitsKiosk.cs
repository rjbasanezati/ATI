using System;
using System.ComponentModel.DataAnnotations;

namespace ATI_IEC.Models
{
    public class FitsKiosk
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "FITS Kiosk")]
        public string KioskName { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        [Display(Name = "Launched Date")]
        public DateTime LaunchedDate { get; set; }
    }
}
