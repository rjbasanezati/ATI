using System.Collections.Generic;

namespace ATI_IEC.Models
{
    public class AdminViewModel
    {
        public List<UpdateItem> TMSUpdates { get; set; }
        public List<UpdateItem> Announcements { get; set; }
        public List<UpdateItem> CalendarEvents { get; set; }
    }
}
