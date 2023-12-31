using AudioWebApp.Client.Models;
using System.Collections.ObjectModel;
using System.Xml.Linq;

#pragma warning disable CS1701 // Assuming assembly reference matches identity

namespace AudioWebApp.Client.Services
{
	static public class XmlReader
	{
		static readonly ObservableCollection<Series> _bookList = new ObservableCollection<Series>();
		static readonly ObservableCollection<Series> _topicList = new ObservableCollection<Series>();
		static readonly List<Server> _serverList = new List<Server>();

		static public ObservableCollection<Series> ReadBooks(XDocument doc)
		{
			var books = from book in doc.Descendants("Book")
				select new Series
				{
					Name = book.Attribute("name").Value,
					Server = book.Attribute("server").Value,
					Messages = new List<Message>(
						from item in book.Descendants("Item")
						select new Message
						{
							Name = item.Attribute("name").Value,
							Link = item.Attribute("link").Value
						})
				};

			foreach (var book in books)
			{
				_bookList.Add(new Series { Name = book.Name, Server = book.Server, Messages = book.Messages });
			}

			return _bookList;
		}

		/// <summary>
		/// Reads the topics.
		/// </summary>
		/// <returns>The topics.</returns>
		static public ObservableCollection<Series> ReadTopics(XDocument doc)
		{
			var topics = from topic in doc.Descendants("Topic")
				select new Series
				{
					Name = topic.Attribute("name").Value,
					Server = topic.Attribute("server").Value,
					Messages = new List<Message>(
						from item in topic.Descendants("Item")
						select new Message
						{
							Name = item.Attribute("name").Value,
							Link = item.Attribute("link").Value
						})
				};

			foreach (var book in topics)
			{
				_topicList.Add(new Series { Name = book.Name, Server = book.Server, Messages = book.Messages });
			}

			return _topicList;
		}


		/// <summary>
		/// Read the books of the bible from the xml file.
		/// </summary>
		static public ObservableCollection<Series> ReadRadio()
		{
			var doc = XDocument.Load("Radio.xml");

			var books = from book in doc.Descendants("Episode")
				select new Series
				{
					Name = book.Attribute("name").Value,
					Server = book.Attribute("server").Value,
					Messages = new List<Message>(
						from item in book.Descendants("Item")
						select new Message
						{
							Name = item.Attribute("name").Value,
							Link = item.Attribute("link").Value
						})
				};

			foreach (var book in books)
			{
				_bookList.Add(new Series { Name = book.Name, Server = book.Server, Messages = book.Messages });
			}

			return _bookList;
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