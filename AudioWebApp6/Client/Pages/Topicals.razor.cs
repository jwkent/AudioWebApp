
using System.Collections.ObjectModel;
using AudioWebApp.Client.Models;


namespace AudioWebApp.Client.Pages
{
    public partial class Topicals
    {
       
        private ObservableCollection<Series>? topics;
        protected override async Task OnInitializedAsync()
        {
            await apiService.LoadData();
            topics = apiService.Topics;
        }
    }
}