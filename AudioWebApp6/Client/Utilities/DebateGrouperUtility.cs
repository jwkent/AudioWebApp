﻿using AudioWebApp.Client.Pages;
using System.Collections.ObjectModel;
using AudioWebApp.Client.Models;
using System.Text.RegularExpressions;

namespace AudioWebApp.Client.Utilities
{
    public class DebateGrouperUtility
    {
        private Dictionary<string, List<Message>> _DebateGroups = new Dictionary<string, List<Message>>();
        public ObservableCollection<DebateGroup> DebateCollection { get; set; } 
        public ObservableCollection<Series> OriginalSeriesCollection { get; set; }
        public void CreateCategories(ObservableCollection<Series> collection)
        {
            DebateCollection = new ObservableCollection<DebateGroup>();
            string collecionServer = collection[0].Server;
            string collectionAbbr = collection[0].Abbr;

            
            // to get the list of messages ie debates in the collection param
            List<Message> messages = new List<Message>();
            foreach (Series series in collection)
            {
                foreach(var message in series.Messages)
                {
                    Message alteredMessage = RemoveNumerical(message); // remove the numbers from the front of the name
                    messages.Add(alteredMessage);
                }  
            }
            
            // Now I have a list of debates as messages  <name and link>
            foreach (var str in messages)
            {
                string category = CategorizeString(str);  //put a message name in -> get back a category
                if(!_DebateGroups.ContainsKey(category))
                {
                    _DebateGroups[category] = new List<Message>();
                }
                _DebateGroups[category].Add(str);
            }

            CreateDebateGroup(_DebateGroups, collecionServer, collectionAbbr);
        }
        private void CreateDebateGroup(Dictionary<string, List<Message>> categories, string server, string abbr )
        {
            foreach (var dgo in categories)
            {
                DebateCollection.Add(new DebateGroup
                {
                    DebateTopic = dgo.Key,
                    Server = server,
                    Abbr = abbr,
                    Messages = new List<Message>(dgo.Value)
                }); 
            }
        }

        private string CategorizeString(Message message)
        {
            if (message.Name.Contains("Calvinism"))
            {
                return "Calvinism Debates";
            }
            else if (message.Name.Contains("Eternal Security"))
            {
                return "Eternal Security Debates";
            }
            else if (message.Name.Contains("Olivet Discourse"))
            {
                return "Olivet Discourse Debates";
            }
            else if (message.Name.Contains("Debate Broadcast"))
            {
                return "Debate Broadcasts";
            }
            else if (message.Name.Contains("Full Preterism"))
            {
                return "Full Preterism Discussions";
            }
            else if (message.Name.Contains("Catholicism"))
            {
                return "Catholicism Debates";
            }
            else if (message.Name.Contains("Replacement Theology"))
            {
                return "Replacement Theology Debates";
            }
            else if (message.Name.Contains("Israel"))
            {
                return "Israel and Christianity Debates";
            }
            else if (message.Name.Contains("Science") || message.Name.Contains("Atheism") || message.Name.Contains("Theism"))
            {
                return "Science and Christianity Debates";
            }
            else
            {
                return "Miscellaneous Debates";
            }
        }

        private Message RemoveNumerical(Message value)
        {
            string pattern = @"^\S+\s"; // Matches non-space characters followed by a space
            Message alteredMessage = new Message
            {
                Name = Regex.Replace(value.Name, pattern, ""),
                Link = value.Link
            };
            return alteredMessage;
        }
    }
}
