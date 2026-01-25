using System;

namespace ATI_IEC.Models
{
    public class CalendarActivity
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public DateTime ActivityDate { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
