using Microsoft.AspNetCore.Components;
using MudBlazor;


using static MudBlazor.CategoryTypes;

namespace AudioWebApp.Client.Pages
{
    public partial class Radio : Microsoft.AspNetCore.Components.ComponentBase
    {
        public string cardContent { get; set; } = "Card Content Title";
        public bool _radioPlaying { get; set; }
        
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
            if (_radioPlaying) { _radioPlaying =  false; };

            DialogService.Show<CallDialog>("Call Into Radio Program",dialogOptions);
        }
    }
}
