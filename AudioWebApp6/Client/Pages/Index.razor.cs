using AudioWebApp.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;


namespace AudioWebApp.Client.Pages
{
    public partial class Index: ComponentBase
    {
        [Inject]
        private SharedDataService sharedDataService { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }
        string appTitle = "The Narrow Path";
        string appCaption = "Enter by the narrow gate; for wide is the gate and broad is the way that leads to destruction, and there are many who go in by it. Because narrow is the gate and difficult is the way which leads to life, and there are few who find it.";
        string citation = "- Jesus of Nazareth (Matthew 7:13-14)";

        
        public void ShowContent()
        {
            sharedDataService.ShowMenu(); 
        }
        public void FeatureNavigateToRadio()
        {
            navigationManager.NavigateTo("/radio");
        }
        public void FeatureNavigateToAudioBooks()
        {
            navigationManager.NavigateTo("/audio-books");
        }
        public void FeatureNavigateToHowTo()
        {
            navigationManager.NavigateTo("/how-to");
        }
    }

}
