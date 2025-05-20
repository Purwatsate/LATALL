using InventoryService.Application.Services.gRPC;
using InventoryService.InfraStructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddGrpc();
builder.Services.AddControllers();
builder.Services.AddDbContext<InventoryDbContext>(options =>
 options.UseNpgsql(builder.Configuration.GetConnectionString("LATALL_Inventory_Connection")));

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5004, listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
    });
});

var app = builder.Build();
app.MapGrpcService<InventoryServiceImpl>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
