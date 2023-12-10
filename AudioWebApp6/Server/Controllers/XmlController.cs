using AudioWebApp.Server.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace AudioWebApp.Server.Controllers;

[Route("api/v1/data")]
public class XmlController : Controller
{
    private const int TimestampMaxLength = 14;
    
    [HttpGet("get")]
    public string GetNewContent()
    {
        return XmlManager.GetXmlFileString();
    }
    
    [HttpGet("force-update")]
    public string ForceUpdate()
    {
        new XmlUpdaterViaJson().UpdateXMLFile(DateTime.Now);
        return "XML file updated.";
    }
    
    [HttpGet("is-new/{clientTimestampString}")]
    public bool IsNewContentAvailable(string clientTimestampString)
    {
        if (clientTimestampString.Length > TimestampMaxLength)
        {
            return false;
        }

        try
        {
            var clientTimeStamp = DateTime.ParseExact(clientTimestampString, "yyyyMMddHHmmss", null);

            return XmlManager.IsUpdatedXmlFileAvailable(clientTimeStamp);
        }
        catch
        {
            return false;
        }
    }
}