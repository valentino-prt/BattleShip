using BattleShip.Api.Api;
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
            builder.WithOrigins("http://localhost:5051")
                .AllowAnyHeader()
                .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding")
                .AllowAnyMethod();
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
// app.MapGrpcService<BattleShipServiceImpl>().EnableGrpcWeb()
//     .RequireCors("AllowAll");


app.UseCors("AllowMyOrigin");

// Endpoints configuration
app.MapGameEndpoints();
app.MapHub<PlayerHub>("/playerHub");

app.Run();