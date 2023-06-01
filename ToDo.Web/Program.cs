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

// notatki:
//Nowy model musi zawierac status
// -new
// -approved
// -in progress
// -completed
// Trzeba stworzyc model, ktory bedzie zawieral te statusy i kazdy status bedzie przypisany do TODO
// Trzeba stworzyc 4 kolumny w wyswietlaniu TODO, kazda kolumna kazdy status
// Trzeba dodac przyciski odpowiedzialne za zmiane statusu
// Po stworzeniu TODO otrzymuje on automatycznie status NEW
// Po nacisnieciu przycisku zmiany statusu, TODO jest przeniesiony do innej kolumny
// TODO data dodania, data zakonczenia

// Pozniejsze plany
// Kazdy TODO moze miec w sobie liste checkboxow, ktore user bedzie mogl zaznaczac, po zaznaczeniu wszystkich TODO leci na status completed
// Kazdy TODO bedzie mogl zzawierac zdjecia, kazdy uzytkownic po nacisnieciu przycisku Complete bedzie mogl dodac notatke, lub zdjecie, to bedzie wystwietlane w Details
// TODO w statusie completed
// User bedzie mogl za pomoca myszki przesunac  TODO w dana kolumne ze statusem
// TODO bedzie mial zapisana historie, z jakich statusow i w jakim czasie trafial