using AudioWebApp.Client.Models;
using System.Collections.ObjectModel;
using System.Xml.Linq;

#pragma warning disable CS1701 // Assuming assembly reference matches identity

namespace AudioWebApp.Client.Services
{
	static public class XmlReader
	{
		static readonly List<Server> _serverList = new List<Server>();

        public static ObservableCollection<Series> ReadData(XDocument doc, string title)
        {
            var items = new ObservableCollection<Series>();

            var categories = from category in doc.Descendants(title)
                         select new Series
                         {
                             Name = category.Attribute("name").Value,
                             Server = category.Attribute("server").Value,
                             Messages = new List<Message>(
                                 from item in category.Descendants("Item")
                                 select new Message
                                 {
                                     Name = item.Attribute("name").Value,
                                     Link = item.Attribute("link").Value
                                 })
                         };

            foreach (var item in categories)
            {
                items.Add(new Series { Name = item.Name, Server = item.Server, Messages = item.Messages });
            }

            return items;
        }
      

		/// <summary>
		/// Reads the servers.
		/// </summary>
		/// <returns>The servers.</returns>
		static public List<Server> ReadServers(XDocument doc)
		{
			var serverList = doc.Descendants("Configuration")
				.Descendants("Servers")
				.Descendants("Server");

			foreach (var server in serverList)
			{
				_serverList.Add(new Server { Name = server.Attribute("name").Value, Location = server.Attribute("location").Value });
			}

			//System.Diagnostics.Debug.WriteLine("ReadServers name: " + _serverList[0].Name);

			return _serverList;
		}
	}
}

#pragma warning restore CS1701 // Assuming assembly reference matches identity