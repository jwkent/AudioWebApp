using Microsoft.AspNetCore.Components;

namespace AudioWebApp.Client.Pages
{
    public partial class Index: ComponentBase
    {
        string appTitle = "Audio Web app";
        private void NavToRadio()
        {
            NavigationManager.NavigateTo("/radio");
        }
        private void NavToExploreContent()
        {
            NavigationManager.NavigateTo("/explorecontent");
        }
    }
}
