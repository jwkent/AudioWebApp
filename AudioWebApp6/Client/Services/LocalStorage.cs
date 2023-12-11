namespace AudioWebApp.Client.Services;

public class LocalStorage
{
    public LocalStorage(){}

    public string FilesPath()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/NarrowPath/";
    }

    public string XmlFilePath()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/NarrowPath/Data.xml";
    }
}