using System;

namespace ATI_IEC.Models
{
    public class UpdateItem
    {
        public int Id { get; set; }
        public string Type { get; set; } // "TMS", "Announcement", "Calendar"
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
