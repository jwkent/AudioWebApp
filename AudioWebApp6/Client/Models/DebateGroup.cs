using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;

namespace AudioWebApp.Client.Models
{
    public class DebateGroup
    {
        public string DebateTopic { get; set; }
        public string Server { get; set; }
        public string Abbr { get; set; }
        public List<Message> Messages { get; set; } 
        
    }
}
