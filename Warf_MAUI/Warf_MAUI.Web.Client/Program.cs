using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Warf_MAUI.Shared.Services;
using Warf_MAUI.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the Warf_MAUI.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

await builder.Build().RunAsync();
