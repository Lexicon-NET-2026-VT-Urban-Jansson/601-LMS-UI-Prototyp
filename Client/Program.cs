using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

using TestClaudeLMS01.Client;
using TestClaudeLMS01.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddRadzenComponents();

builder.Services.AddRadzenCookieThemeService(options =>
{
    options.Name = "TestClaudeLMS01Theme";
    options.Duration = TimeSpan.FromDays(365);
});

// Registrera AuthState och MockDataService
builder.Services.AddSingleton<AuthState>();
builder.Services.AddSingleton<MockDataService>();

builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var host = builder.Build();
await host.RunAsync();