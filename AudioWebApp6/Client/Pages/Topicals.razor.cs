using AudioWebApp.Shared;
using System.Net.Http.Json;

namespace AudioWebApp.Client.Pages
{
    public partial class Topicals
    {


        private Topical[]? topicals;

        protected override async Task OnInitializedAsync()
        {
            topicals = await Http.GetFromJsonAsync<Topical[]>("api/gettopicals");

        }
    }
}