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
        private const string ApiBibleOverviewsEndpoint = "bible-book-overviews";
        private const string ApiDebatesAndInterviewsEndpoint = "debates-and-interviews";
        private const string ApiIndividualTeachingsEndpoint = "individual-topical-teachings";
        private const string ApiTeachingsOfChristEndpoint = "the-life-and-teachings-of-christ";

        private static List<Item> GetData(string apiEndPoint)
        {
            var response = ServerName
                .AppendPathSegment(ApiAudioPath)
                .SetQueryParams(new { category = apiEndPoint })
                .GetJsonAsync<Data>();

            return response.Result.Results;
        }
        
        public static List<Item> GetVerseByVerseData()
        {
            return GetData(ApiVerseByVerseEndpoint);
        }

        public static List<Item> GetTopicalData()
        {
            return GetData(ApiTopicalLecturesEndpoint);
        }
        
        public static List<Item> GetBibleOverviewsData()
        {
            return GetData(ApiBibleOverviewsEndpoint);
        }
        
        public static List<Item> GetDebatesAndInterviewsData()
        {
            return GetData(ApiDebatesAndInterviewsEndpoint);
        }
        
        public static List<Item> GetIndividualTeachingsData()
        {
            return GetData(ApiIndividualTeachingsEndpoint);
        }
        
        public static List<Item> GetTeachingsOfChristData()
        {
            return GetData(ApiTeachingsOfChristEndpoint);
        }
    }
}
