using Microsoft.AspNetCore.Components;
using MudBlazor;


using static MudBlazor.CategoryTypes;

namespace AudioWebApp.Client.Pages
{
    public partial class Radio : Microsoft.AspNetCore.Components.ComponentBase
    {
        bool _radioPlaying;
        
        private void PlayRadio()
        {
            _radioPlaying = !_radioPlaying;
        }

        DialogOptions disableBackdropClick = new DialogOptions()
        { DisableBackdropClick = true };
        private void OpenDialog()
        {
            if (_radioPlaying) { _radioPlaying =  false; };

            DialogService.Show<CallDialog>();
        }
    }
}
