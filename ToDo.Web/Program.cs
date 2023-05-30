using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ToDo.Services.Services;
using ToDo.Services.Services.Contracts;
using ToDo.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7265/") });
builder.Services.AddScoped<IToDoItemService, ToDoItemService>();
builder.Services.AddBootstrapBlazor();

await builder.Build().RunAsync();
