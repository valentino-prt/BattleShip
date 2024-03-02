using BattleShip.Api.Api;
using BattleShip.Api.Grpc;
using BattleShip.Api.Hubs;
using BattleShip.Api.Services;
using BattleShip.Api.Utils;

var builder = WebApplication.CreateBuilder(args);

// Services configuration
builder.Services.AddSingleton<GameService>(); // Gestion des jeux
builder.Services.AddSingleton<ConnectionMapping>(); // Gestion des connexions

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// SignalR configuration
builder.Services.AddSignalR();

// gRPC configuration
builder.Services.AddGrpc();


// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


// Activation de Swagger UI in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGrpcService<BattleShipServiceImpl>()
    .EnableGrpcWeb();
app.UseGrpcWeb();

app.UseCors();

// Endpoints configuration
app.MapGameEndpoints();
app.MapHub<PlayerHub>("/playerHub");

app.Run();