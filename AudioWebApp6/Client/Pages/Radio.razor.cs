using AudioWebApp.Client.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;


namespace AudioWebApp.Client.Pages
{
    public partial class Radio : Microsoft.AspNetCore.Components.ComponentBase
    {
        [Inject]
        SharedDataService SharedDataService { get; set; }

        string? dataReady;
        string testAudioTitle = DateTime.Now.ToString("MM/dd/yyyy");
        string? testAudioSource = "";
        RadioObject radioObject;

        DialogOptions dialogOptions = new DialogOptions()
        { DisableBackdropClick = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true
        };
        private void OpenDialog()
        {
            DialogService.Show<CallDialog>("Call Into Radio Program",dialogOptions);
        }
        protected override async Task OnInitializedAsync()
        {
            radioObject = new RadioObject(testAudioTitle, testAudioSource);
            dataReady = "dataLoaded";
        }
        
        // Change when actual values are being used. 
        public void OpenAudio(object audioTitle, object audioSource)
        {
            SharedDataService.AudioLink = audioSource.ToString();
            SharedDataService.AudioTitle = audioTitle.ToString();
            SharedDataService.TogglePlayer();
        }
    }
    public class RadioObject
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public RadioObject(string name, string source) 
        {
            Name = name;
            Source = source;
        }
    }
}
