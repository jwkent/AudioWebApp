using System.Xml.Linq;
using AudioWebApp.Server.Models;

namespace AudioWebApp.Server.Utilities
{
    public static class XmlManager
    {
        public static string GetXmlFileString()
        {
            return File.ReadAllText(SharedData.CurrentData);
        }

        /// <summary>
        /// Checks to see if the cashed XML file is newer
        /// than the clients.
        /// </summary>
        /// <param name="clientTimestamp"></param>
        /// <returns></returns>
        public static bool IsUpdatedXmlFileAvailable(DateTime clientTimestamp)
        {
            DateTime lastWriteTime = GetCurrentXmlDateTime();
            return lastWriteTime > clientTimestamp;
        }

        /// <summary>
        /// Checks to see if the database is updated.
        /// </summary>
        /// <param name="dbTimestamp"></param>
        /// <returns></returns>
        public static bool IsDatabaseUpdated(DateTime dbTimestamp)
        {
            DateTime lastWriteTime = GetCurrentXmlDateTime();
            return dbTimestamp > lastWriteTime;
        }

        /// <summary>
        /// Gets the current xml file's last write time.
        /// </summary>
        /// <returns></returns>
        private static DateTime GetCurrentXmlDateTime()
        {
            var doc = XDocument.Load(SharedData.CurrentData);
            var configNode = doc.Descendants("Configuration");
            var lastUpdatedNode = configNode.Descendants("LastUpdated");
            var dateTimeNode = lastUpdatedNode.Attributes("dateTime");
            string dateTimeString = dateTimeNode.First().Value;

            DateTime lastWriteTime = (DateTime.ParseExact(dateTimeString, "yyyyMMddHHmmss", null));
            return lastWriteTime;
        }
    }
}