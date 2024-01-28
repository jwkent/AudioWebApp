using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AudioWebApp.Client.Pages
{
    public partial class CallDialog : ComponentBase
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        void Cancel() => MudDialog.Cancel();
        private string telephoneNumber = "Tel:844-484-5737";
    }
}
