using BattleShip.Api.Api;
using BattleShip.Api.Hubs;
using BattleShip.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<GameService>();

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

app.MapGameEndpoints();
app.MapHub<PlayerHub>("/playerhub");


app.Run();