using AudioWebApp.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;

namespace AudioWebApp.Client.Services
{
    public class CompletedService
    {
        private readonly IJSRuntime _jsruntime;
        

        public CompletedService(IJSRuntime JSRuntime) 
        {
            _jsruntime = JSRuntime;
        }
        public async void CheckForCompleted(string seriesName)
        {
           // generate a list of completed categroy messages via key seriesName
            // javascript with seriesName as key to look up.

        }
    }
}
