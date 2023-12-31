using MudBlazor;

namespace AudioWebApp.Client.Pages
{
    public partial class Radio : Microsoft.AspNetCore.Components.ComponentBase
    {
        AudioCard? AudioCardControl;
        public void CloseAudioPlayer()
        {
            AudioCardControl?.ClosePlayer();
        }
        public string cardContent { get; set; } = "Card Content Title";
        private bool _radioPlaying { get; set; }
        
        private void PlayRadio()
        {
            _radioPlaying = !_radioPlaying;   
        }
        
        DialogOptions dialogOptions = new DialogOptions()
        { DisableBackdropClick = true,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true
        };
        private void OpenDialog()
        {
            if (_radioPlaying == true) { CloseAudioPlayer(); };

            DialogService.Show<CallDialog>("Call Into Radio Program",dialogOptions);
        }
    }
}
