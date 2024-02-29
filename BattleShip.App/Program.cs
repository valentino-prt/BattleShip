using BattleShip.App;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//TODO changer la définition du client http où on défini l'adresse de base en tant que celle de l'api
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
GameState.InitializeInstance(Guid.NewGuid(), new char[10, 10], new char[10, 10], Guid.NewGuid());
builder.Services.AddSingleton(GameState.Instance);

await builder.Build().RunAsync();