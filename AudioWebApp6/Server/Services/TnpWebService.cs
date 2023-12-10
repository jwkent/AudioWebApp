using AudioWebApp.Server.Models;
using Flurl;
using Flurl.Http;

namespace AudioWebApp.Server.Services
{
    public class TnpWebService
    {
        private const string ServerName = "http://thenarrowpath.com";
        private const string ApiAudioPath = "api/v1.0/audio";
        private const string ApiVerseByVerseEndpoint = "verse-by-verse";
        private const string ApiTopicalLecturesEndpoint = "topical-lectures";

        /// <summary>
        /// Get verse by verse data from the web service.
        /// </summary>
        /// <returns></returns>
        public static List<Item> GetVerseByVerseData()
        {
            var response = ServerName
                .AppendPathSegment(ApiAudioPath)
                .SetQueryParams(new { category = ApiVerseByVerseEndpoint })
                .GetJsonAsync<Data>();

            return response.Result.Results;
        }

        /// <summary>
        /// Get verse by verse data from the web service.
        /// </summary>
        /// <returns></returns>
        public static List<Item> GetTopicalData()
        {
            var response = ServerName
                .AppendPathSegment(ApiAudioPath)
                .SetQueryParams(new { category = ApiTopicalLecturesEndpoint })
                .GetJsonAsync<Data>();

            return response.Result.Results;
        }
    }
}
