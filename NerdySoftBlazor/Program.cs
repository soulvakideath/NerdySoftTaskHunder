using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;
using MudBlazor.Services;
using NerdySoftBlazor;
using NerdySoftBlazor.Service;
using NerdySoftBlazor.Service.IServices;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5225/") });

builder.Services.AddScoped<HttpService>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddMudServices();
await builder.Build().RunAsync();