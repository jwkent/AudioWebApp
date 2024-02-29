namespace AudioWebApp.Server.Utilities
{
    public class AnnouncementsManager
    {
        public IResult GetAnnouncementsFile()
        {
            string filePath = "./Files/AnnouncementsData.json";
            if(File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                return Results.Json(jsonContent);
            }
            else
            {
                return Results.NotFound("No content found"); 
            }
        }
    }
}
