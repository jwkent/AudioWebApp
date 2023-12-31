namespace AudioWebApp.Client.Models;

public class Series
{
    public string Name { get; set; }
    public string Server { get; set; }
    public string Abbr { get; set; }
    public List<Message> Messages { get; set; }
}
