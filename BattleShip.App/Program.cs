using BattleShip.App;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
GameState.InitializeInstance(Guid.NewGuid(), new char[10, 10], new char[10, 10]);
builder.Services.AddSingleton(GameState.Instance);

await builder.Build().RunAsync();