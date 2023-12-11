using System.Collections.ObjectModel;
using System.Xml.Linq;
using AudioWebApp.Client.Models;

namespace AudioWebApp.Client.Services;

public class ApiService
{
    const string ServerDomain = "http://tnp.wvss.biz";
    const string ServerVersion = "v1";
    private const string BaseAddress = $"{ServerDomain}/{ServerVersion}";
    private const string updatedContentCheckUrl = $"{BaseAddress}/data/get";
    //private const string updatedContentCheckUrl = "data/get";
    private const string isNewContentCheckUrl = $"{BaseAddress}/data/is-new";
    //private const string isNewContentCheckUrl = "data/is-new";

    private HttpClient _httpClient;

    public List<Server> Servers;
    public ObservableCollection<Series> Topics;
    public ObservableCollection<Series> Books;
    
    readonly string xmlFilePath = new LocalStorage().XmlFilePath();
    readonly string filesPath = new LocalStorage().FilesPath();

    public ApiService()
    {
    }

    public async Task LoadData()
    {
        _httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(5) };
        
        // Continue with is used to synchronously load XML data into data model, if necessary.
        await LoadXmlDataAsync()
            .ContinueWith((arg) =>
            {   // Run after data is loaded either locally or from web service.
                Servers = XmlReader.ReadServers(arg.Result);
                Topics = XmlReader.ReadTopics(arg.Result);
                Books = XmlReader.ReadBooks(arg.Result);
                //IsSeriesReady = true;
                //System.Diagnostics.Debug.WriteLine("VM back from LoadXmlDataAsync in continue with.");
            });
        
    }

    // public async Task<IEnumerable<Customer>> GetCustomersAsync()
    // {
    //     return await _httpClient.GetFromJsonAsync<IEnumerable<Customer>>("https://yourapi.com/customers");
    // }
    
    /// <summary>
    /// Loads the xml data async.
    /// </summary>
    /// <returns>The xml data async.</returns>
    private async Task<XDocument> LoadXmlDataAsync()
    {
        //System.Diagnostics.Debug.WriteLine("LoadXmlDataAsync starting...");

        // If new content is available via the web service.
        var result = await IsNewContentAvailable();
        if (result)
        {
            try
            {
                // Download new XML content.
            await _httpClient.GetStringAsync(updatedContentCheckUrl)
                .ContinueWith((Task<string> arg) => 
                {
                    XDocument doc;
                    try
                    {
                        doc = XDocument.Parse(arg.Result);
                    }
                    catch (Exception)
                    {   // The XML data received from the web service was invalid.
                        return;
                    }
                    // The XML data received was valid, so save it as the new local data model.
                    SaveXmlFile(doc.ToString());
                    
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //System.Diagnostics.Debug.WriteLine("isNewContentAvailable finished");

        // Load local file which may have recently been updated with new content.
        return LoadLocalXmlFile();
    }
    
    /// <summary>
    /// Is there any new content available.
    /// </summary>
    /// <returns>The new content available.</returns>
    async Task<bool> IsNewContentAvailable()
    {
        try
        {
            // Read the local XML file timestamp and send it to the web service to see if there is any new content available.
            if (File.Exists(xmlFilePath))
            {
                var localDataTimestamp = LoadLocalXmlFile()
                    .Descendants("Configuration")
                    .Descendants("LastUpdated")
                    .Attributes("dateTime")
                    .First()
                    .Value;

                bool.TryParse(await _httpClient.GetStringAsync($"{isNewContentCheckUrl}/{localDataTimestamp}"), out var boolResult);
                return boolResult;
            }
            // There is no data file available, so get one from the webservice.
            return true;
				
        }
        catch (Exception )
        {
            //System.Diagnostics.Debug.WriteLine("isNewContentAvailable exception {0}", e.Message);

            // Likely a 404 error has occurred.  The web service is not available so use local data model i.e. no new data available.
            return false;
        }
    }
    
    /// <summary>
    /// Loads the local xml file.
    /// </summary>
    /// <returns>The local xml file.</returns>
    XDocument LoadLocalXmlFile()
    {
        return XDocument.Load(xmlFilePath);
    }

    /// <summary>
    /// Saves the xml file.
    /// </summary>
    /// <param name="xmlString">Xml string.</param>
    void SaveXmlFile(string xmlString)
    {
        var path = filesPath;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(xmlFilePath, xmlString);
    }
}
