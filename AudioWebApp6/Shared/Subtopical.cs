using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWebApp.Shared
{
    public class Subtopical
    {
        public string Title { get; set; }
        public string AudioFile { get; set; }
        public Subtopical(string title, string audioFile) 
        {
            Title = title;
            AudioFile = audioFile;
        }
    }
}
