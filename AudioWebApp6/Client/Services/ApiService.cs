using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Xml.Linq;
using AudioWebApp.Client.Models;
using Microsoft.JSInterop;

namespace AudioWebApp.Client.Services;

public class ApiService
{
    //const string ServerDomain = "http://tnp.wvss.biz";
    const string ServerDomain = "http://localhost:5000";
    const string ServerVersion = "api/v1";
    private const string BaseAddress = $"{ServerDomain}/{ServerVersion}";
    //private const string updatedContentCheckUrl = $"{BaseAddress}/data/get";
    private const string updatedContentCheckUrl = $"{BaseAddress}/data";
    //private const string updatedContentCheckUrl = "data/get";
    private const string isNewContentCheckUrl = $"{BaseAddress}/data/is-new";
    //private const string isNewContentCheckUrl = "data/is-new";

    private HttpClient _httpClient;

    public List<Server> Servers;
    public ObservableCollection<Series> Topics;
    public ObservableCollection<Series> Books;

    private readonly IJSRuntime jSRuntime;

    public ApiService(IJSRuntime JSRuntime)
    {
        jSRuntime = JSRuntime;
    }

    public async Task LoadData()
    {
        _httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(5) };
        Console.WriteLine("hi");
        

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

   

    //public async Task<IEnumerable<Customer>> GetCustomersAsync()
    //{
    //    return await _httpClient.GetFromJsonAsync<IEnumerable<Customer>>("https://yourapi.com/customers");
    //}

    /// <summary>
    /// Loads the xml data async.
    /// </summary>
    /// <returns>The xml data async.</returns>
    private async Task<XDocument> LoadXmlDataAsync()
    {
        Console.WriteLine("LoadXmlDataAsync starting...");

        // If new content is available via the web service.
        var result = await IsNewContentAvailable();
        if (result)
        {
            try
            {
                Console.WriteLine($"About to get some content {updatedContentCheckUrl}");
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
                Console.WriteLine("After getting some content.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //System.Diagnostics.Debug.WriteLine("isNewContentAvailable finished");

        // Load local file which may have recently been updated with new content.
        return await LoadLocalXmlFile();
    }
    
    /// <summary>
    /// Is there any new content available.
    /// </summary>
    /// <returns>The new content available.</returns>
    async Task<bool> IsNewContentAvailable()
    {
        try
        {
            var data = await GetItemAsync("xmlString");
            if (!string.IsNullOrEmpty(data))
            // Read the local XML file timestamp and send it to the web service to see if there is any new content available.
            {
                var doc = await LoadLocalXmlFile();
                var localDataTimestamp = doc
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
    async Task<XDocument> LoadLocalXmlFile()
    {
        var xmlString = await GetItemAsync("xmlString");
        if (!string.IsNullOrEmpty(xmlString))
        {
            var doc = XDocument.Parse(xmlString);
            return doc;
        }
        return new XDocument();
        // return XDocument.Load(xmlFilePath);
    }

    /// <summary>
    /// Saves the xml file.
    /// </summary>
    /// <param name="xmlString">Xml string.</param>
    async void SaveXmlFile(string xmlString)
    {
        Console.WriteLine("About to save xml.");
        await SetItemAsync("xmlString", xmlString);
        Console.WriteLine("Done save xml.");

        //var path = filesPath;
        //if (!Directory.Exists(path))
        //{
        //    Directory.CreateDirectory(path);
        //}
        //File.WriteAllText(xmlFilePath, xmlString);
    }

    // Example method to set an item
    private async Task SetItemAsync(string key, string value)
    {
        await jSRuntime.InvokeVoidAsync("localforage.setItem", key, value);
    }

    // Example method to get an item
    private async Task<string> GetItemAsync(string key)
    {
        return await jSRuntime.InvokeAsync<string>("localforage.getItem", key);
    }

    //private string dbName = "tnpDb";

    //private async Task OpenDatabaseAsync()
    //{
    //    await jSRuntime.InvokeVoidAsync("indexedDBInterop.openDb", dbName, 1);
    //}

    //private async Task AddDataAsync(string xmlString)
    //{
    //    var db = await jSRuntime.InvokeAsync<object>("indexedDBInterop.openDb", dbName, 1);
    //    await jSRuntime.InvokeVoidAsync("indexedDBInterop.addData", db, "xmlFile", xmlString);
    //}

    //private async Task<string> GetDataAsync()
    //{
    //    var db = await jSRuntime.InvokeAsync<object>("indexedDBInterop.openDb", dbName, 1);
    //    var data = await jSRuntime.InvokeAsync<string>("indexedDBInterop.getData", db, "xmlFile", 1);
    //    return data;
    //}

    //private async Task<bool> DatabaseExistsAsync()
    //{
    //    return await jSRuntime.InvokeAsync<bool>("indexedDBInterop.dbExists", dbName);
    //}

    //private async Task OpenOrCreateDatabaseAsync()
    //{
    //    bool dbExists = await DatabaseExistsAsync();
    //    if (!dbExists)
    //    {
    //        // Logic to create a new database or perform first-time setup
    //    }
    //    // Open the database (assuming it exists or was just created)
    //}
}
