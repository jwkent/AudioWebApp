using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWebApp.Shared.Models
{
    public class Announcement
    {
        public string? Title { get; set; }
        public bool IsNew { get; set; }
        public string BodyText { get; set; }
    }
}
