using BattleShip.Api.Models;
using BattleShip.Api.Services;
using Microsoft.AspNetCore.Mvc;
using BattleShip.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IGameService, GameService>();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
    builder =>
    {
        builder.WithOrigins("http://localhost:5051")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowMyOrigin");


// Endpoint pour créer une nouvelle partie
app.MapPost("/games", (IGameService gameService) =>
{
    GameInfo gameInfo = gameService.CreateGame();
    return TypedResults.Ok(gameInfo);
})
.WithName("CreateGame")
.WithOpenApi();

// Endpoint pour attaquer une case
app.MapPut("/games/{gameId}/attack", (Guid gameId, [FromBody] AttackRequest request, IGameService gameService) =>
{
    try
    {
        GameInfo result = gameService.Attack(gameId, request.X, request.Y);
        return TypedResults.Ok(result);
    }
    catch (ArgumentException ex)
    {
        return Results.NotFound(ex.Message);
    }
})
.WithName("Attack")
.WithOpenApi();


app.Run();

public record AttackRequest(int X, int Y);
