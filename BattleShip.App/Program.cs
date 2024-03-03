using BattleShip.Api.Grpc;
using BattleShip.App;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using BattleShip.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
    {
        var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
        var channel =
            GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpClient = httpClient });
        return new BattleShipService.BattleShipServiceClient(channel);
    }
);

const string apiUrl = "https://localhost:5000";
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });

GameState.InitializeInstance(Guid.NewGuid(), new char[10, 10], new char[10, 10], Guid.NewGuid(), false, GameMode.SoloVsAi );
builder.Services.AddSingleton(GameState.Instance);

await builder.Build().RunAsync();

//TODO validation API
//TODO Faire fin de partie Win + rediriger home apres la fin et popup gagnant
