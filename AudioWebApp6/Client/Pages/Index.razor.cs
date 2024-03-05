using AudioWebApp.Client.Services;
using Microsoft.AspNetCore.Components;

namespace AudioWebApp.Client.Pages
{
    public partial class Index: ComponentBase
    {
        string? dataReady;
        
        protected override async Task OnInitializedAsync()
        {
            await announcementService.LoadAnnouncements();
            dataReady = "dataLoaded";
        }
    }
}
