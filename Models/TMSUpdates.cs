using System;

namespace ATI_IEC.Models
{
    public class TMSUpdate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
