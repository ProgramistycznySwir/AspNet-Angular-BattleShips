using HappyTeam_BattleShips.Data;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using HappyTeam_BattleShips.Services;


HappyTeam_BattleShips.Models.Game.GenerateBoard();
return;

#region >>> builder config <<<
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// DB:
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Filename=HappyTeam-BattleShips.db"));

DependancyInjection.RegisterDependancies(builder);

// Swagger:
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
     c.SwaggerDoc("v1", new OpenApiInfo {
         Title = "PizzaStore API",
         Description = "Making the Pizzas you love",
         Version = "v1" });
});

builder.Services.AddControllersWithViews();
#endregion >>> builder config <<<

#region >>> app config <<<
var app = builder.Build();


// Swagger:
app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "BattleShips API V1");
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseWebSockets(new WebSocketOptions {
        KeepAliveInterval = TimeSpan.FromMinutes(2)
    });
// app.Run(async (context) =>
// {
//     using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
//     var socketFinishedTcs = new TaskCompletionSource<object>();

//     BackgroundSocketProcessor.AddSocket(webSocket, socketFinishedTcs);

//     await socketFinishedTcs.Task;
// });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.Run();
#endregion >>> app config <<<
