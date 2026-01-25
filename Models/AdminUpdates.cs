using System;
using System.ComponentModel.DataAnnotations;

namespace ATI_IEC.Models
{
    public class AdminUpdate
    {
        public int Id { get; set; }

        [Display(Name = "TMS Updates")]
        public string TMSUpdates { get; set; }

        [Display(Name = "Announcements")]
        public string Announcements { get; set; }

        [Display(Name = "Calendar Events")]
        public string CalendarEvents { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
