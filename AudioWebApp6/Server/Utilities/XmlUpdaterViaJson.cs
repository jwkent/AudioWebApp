using System.Text;
using System.Xml;
using AudioWebApp.Server.Models;
using AudioWebApp.Server.Services;

namespace AudioWebApp.Server.Utilities
{
    public class XmlUpdaterViaJson
    {

        /// <summary>
        /// Creates the xml nodes based on the
        /// passed in parameters.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="rootNode"></param>
        /// <param name="seriesType"></param>
        void CreateSeriesXmlNodes(XmlDocument doc, XmlNode rootNode, string seriesType)
        {
            var seriesQueryJson = seriesType == "Book" ? TnpWebService.GetVerseByVerseData() : TnpWebService.GetTopicalData();

            XmlNode teachingNode = doc.CreateNode(XmlNodeType.Element, "Teaching", null);
            rootNode.AppendChild(teachingNode);

            foreach (var series in seriesQueryJson)
            {
                // Don't update the xml file if the data is null.
                if (series.Items == null
                    || series.Path == null
                    || series.Title == null
                    || series.Type == null
                    || series.Url == null)
                {
                    return;
                }

                XmlNode bookNode = doc.CreateNode(XmlNodeType.Element, seriesType, null);
                teachingNode.AppendChild(bookNode);

                XmlAttribute nameAttribute = doc.CreateAttribute("name");
                nameAttribute.Value = series.Title;
                bookNode.Attributes.SetNamedItem(nameAttribute);

                XmlAttribute serverAttribute = doc.CreateAttribute("server");
                var serverName = series.Path.Contains("thenarrowpath.com") ? "tnp" : "theos";
                serverAttribute.Value = serverName;
                bookNode.Attributes.SetNamedItem(serverAttribute);

                foreach (var m in series.Items.OrderBy(m => m.Sequence))
                {
                    XmlNode itemNode = doc.CreateNode(XmlNodeType.Element, "Item", null);
                    bookNode.AppendChild(itemNode);

                    XmlAttribute itemLinkAttribute = doc.CreateAttribute("link");
                    var linkWithoutServerName = RemoveServerName(m.Url);
                    itemLinkAttribute.Value = linkWithoutServerName;
                    itemNode.Attributes.SetNamedItem(itemLinkAttribute);

                    XmlAttribute itemNameAttribute = doc.CreateAttribute("name");
                    itemNameAttribute.Value = m.Title;
                    itemNode.Attributes.SetNamedItem(itemNameAttribute);
                }
            }
        }

        /// <summary>
        /// Updates the XML file with
        /// new content from the database.
        /// </summary>
        public void UpdateXMLFile(DateTime newTimeStamp)
        {
            var servers = new List<Models.Server>
            {
                new() {Id = 1, Location = "http://media.theos.org/Steve%20Gregg/", Name = "theos"},
                new() {Id = 2, Location = "http://www.thenarrowpath.com/", Name = "tnp"},
                new() {Id = 3, Location = "-", Name = "unknown"},
            };

            XmlDocument doc = new XmlDocument();

            XmlNode rootNode = doc.CreateNode(XmlNodeType.Element, "Data", null);
            doc.AppendChild(rootNode);

            XmlNode configurationNode = doc.CreateNode(XmlNodeType.Element, "Configuration", null);
            rootNode.AppendChild(configurationNode);

            XmlNode lastUpdatedNode = doc.CreateNode(XmlNodeType.Element, "LastUpdated", null);
            configurationNode.AppendChild(lastUpdatedNode);

            XmlAttribute dateTimeAttribute = doc.CreateAttribute("dateTime");
            dateTimeAttribute.Value = newTimeStamp.ToString(@"yyyyMMddHHmmss");
            lastUpdatedNode.Attributes.SetNamedItem(dateTimeAttribute);

            XmlNode serversNode = doc.CreateNode(XmlNodeType.Element, "Servers", null);
            configurationNode.AppendChild(serversNode);

            foreach (var server in servers)
            {
                XmlNode serverNode = doc.CreateNode(XmlNodeType.Element, "Server", null);
                serversNode.AppendChild(serverNode);

                XmlAttribute nameAttribute = doc.CreateAttribute("name");
                nameAttribute.Value = server.Name;
                serverNode.Attributes.SetNamedItem(nameAttribute);

                XmlAttribute locationAttribute = doc.CreateAttribute("location");
                locationAttribute.Value = server.Location;
                serverNode.Attributes.SetNamedItem(locationAttribute);
            }

            // Build the book and topic sections in the XML file.
            CreateSeriesXmlNodes(doc, rootNode, "Topic");
            CreateSeriesXmlNodes(doc, rootNode, "Book");

            string xmlString = null;
            StringBuilder builder = new StringBuilder();
            using (TextWriter wr = new EncodingStringWriter(builder, Encoding.UTF8))
            {
                doc.Save(wr);
                xmlString = wr.ToString();
            }

            // This code prevents users from getting a partially 
            // built xml file.
            File.WriteAllText(SharedData.NewData, xmlString, Encoding.UTF8);
            File.Copy(SharedData.NewData, SharedData.CurrentData, true);

        }

        /// <summary>
        /// Remove the server name from the path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private string RemoveServerName(string path)
        {
            const string removeString = "thenarrowpath.com/";

            if (string.IsNullOrEmpty(path))
            {
                return path;
            }
            var index = path.IndexOf(removeString, StringComparison.Ordinal);
            var cleanPath = (index < 0) ? path : path.Remove(index, removeString.Length);

            return cleanPath;
        }
    }
}
