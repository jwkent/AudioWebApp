using AudioWebApp.Client;
using AudioWebApp.Client.Services;
using AudioWebApp.Client.Utilities;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddScoped<ApiService>();
builder.Services.AddSingleton<SharedDataService>();
builder.Services.AddScoped<FavoritesService>();
builder.Services.AddScoped<LinkShareService>();

builder.Services.AddSingleton<CollectionFilter>();
builder.Services.AddScoped<AudioBookService>();
builder.Services.AddTransient<DebateGrouperUtility>();
builder.Services.AddMudBlazorDialog();

await builder.Build().RunAsync();