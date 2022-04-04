using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CM.WebClient;
using CM.WebClient.Services.Classes;
using CM.WebClient.Services.Interfaces;
using MudBlazor.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiUrl"]) });

builder.Services
               .AddScoped<IAuthenticationService, AuthenticationService>()
               .AddScoped<IHttpService, HttpService>()
               .AddScoped<IPersonService,PersonService>()
               .AddScoped<ILocalStorageService, LocalStorageService>()
               .AddScoped<IChatService,ChatService>();


await builder.Build().RunAsync();

