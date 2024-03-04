using AudioWebApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using MudBlazor;
using System.Text.Json;
namespace AudioWebApp.Server.Utilities
{
    public class AnnouncementsManager
    {
        public string filePath = "./Files/AnnouncementData.json";
        public IResult GetAnnouncementsFile()
        {
            
            if(File.Exists(filePath))
            {
                string jsonContent = File.ReadAllText(filePath);
                object parsedContent = JsonSerializer.Deserialize<object>(jsonContent);
                return Results.Json(parsedContent);
            }
            else
            {
                return Results.NotFound("No content found"); 
            }
        }
        
        public bool Update(DateTime date)
        {
            bool isUpdate;
            if (File.Exists(filePath))
            {
                DateTime creationDateTime = File.GetCreationTimeUtc(filePath);
                
                return isUpdate = creationDateTime > date ? true : false;
            }
            else
            {
                return isUpdate = false;
            }
            
            
        }
        
    }
}
