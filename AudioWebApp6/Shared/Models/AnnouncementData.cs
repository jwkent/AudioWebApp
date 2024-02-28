using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWebApp.Shared.Models
{
    public class AnnouncementData
    {
        public DateTime DateStamp { get; set; }
        public Announcement[] Announcements { get; set; }

        public AnnouncementData( Announcement[] announcements)
        {
            DateStamp = DateTime.Now;
            Announcements = announcements;
        }
    }
}
