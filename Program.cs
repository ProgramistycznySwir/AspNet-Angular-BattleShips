using HappyTeam_BattleShips.Data;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;


#region >>> builder config <<<
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Filename=HappyTeam-BattleShips.db"));

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


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.Run();
#endregion >>> app config <<<
