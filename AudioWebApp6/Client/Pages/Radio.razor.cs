using AudioWebApp.Client.Services;
using MudBlazor;

namespace AudioWebApp.Client.Pages
{
    public partial class Radio : Microsoft.AspNetCore.Components.ComponentBase
    {
        DialogOptions dialogOptions = new DialogOptions()
        { DisableBackdropClick = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true
        };
        private void OpenDialog()
        {
            DialogService.Show<CallDialog>("Call Into Radio Program",dialogOptions);
        }
        
        // Change when actual values are being used. 
        // Alter method click event on frontend UI
        // public void OpenAudio(object audioName, object audioLink){}
        public void OpenAudio()
        {
            //SharedDataService.AudioLink = @fakeAudioLink;
            //SharedDataService.AudioTitle = @fakeAudioName;
            //SharedDataService.TogglePlayer();
        }
    }
}
