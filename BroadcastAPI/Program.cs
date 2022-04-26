using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BroadcastAPI.Data;
using BroadcastAPI;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BroadcastAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BroadcastAPIContext") ?? throw new InvalidOperationException("Connection string 'BroadcastAPIContext' not found.")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.MapGet("/hello", () => "Hello, World!");

app.MapMessageEndpoints();

app.Run();
