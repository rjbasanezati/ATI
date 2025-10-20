using System;
using System.ComponentModel.DataAnnotations;

namespace ATI_IEC.Models
{
    public class RequestFormModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Office { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Purpose of Request")]
        public string Purpose { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Date Requested")]
        [DataType(DataType.Date)]
        public DateTime DateRequested { get; set; }

        [Required]
        [Display(Name = "Date of Distribution")]
        [DataType(DataType.Date)]
        public DateTime DateOfDistribution { get; set; }

        [Required]
        [Display(Name = "Target Recipients")]
        public string TargetRecipients { get; set; } = string.Empty;
        public List<string>? RcefRiceSelected { get; set; }
        public List<string>? CFIDPSelected { get; set; }
        public List<string>? LivestockSelected { get; set; }
        public List<string>? HVCDPSelected { get; set; }
        public List<string>? OrganicSelected { get; set; }
        public string? OthersSelected { get; set; }

    }
}
