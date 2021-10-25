using CM.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Blazor;


namespace CM.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTI0NjE5QDMxMzkyZTMzMmUzMGlEQ254aTd3NkZzVUNrN3pCclBiMlZnakdMUGdscGpIOHg4cXhIZlpIT0E9");

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<CustomStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSyncfusionBlazor();
            await builder.Build().RunAsync();
        }
    }
}
