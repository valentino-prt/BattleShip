using BattleShip.Api.Api;
using BattleShip.Api.Grpc;
using BattleShip.Api.Hubs;
using BattleShip.Api.Services;
using BattleShip.Api.Utils;

var builder = WebApplication.CreateBuilder(args);

// Services configuration
builder.Services.AddSingleton<GameService>(); // Gestion des jeux
builder.Services.AddSingleton<ConnectionMapping>(); // Gestion des connexions

// SignalR configuration
builder.Services.AddSignalR();

// gRPC configuration
builder.Services.AddGrpc();


// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins("http://localhost:5001")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
    });
});

var app = builder.Build();


// Activation de Swagger UI in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseGrpcWeb();
app.UseCors("AllowMyOrigin");
app.MapGrpcService<BattleShipServiceImpl>().EnableGrpcWeb()
    .RequireCors("AllowAll");

// Endpoints configuration
app.MapGameEndpoints();
app.MapHub<PlayerHub>("/playerHub");

app.Run();