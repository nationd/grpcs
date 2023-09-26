using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ToDoGrpc.Data;
using ToDoGrpc.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvcCore()
        .AddApiExplorer();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite("Data Source=ToDoDatabase.db;"));

builder.Services.AddGrpc().AddJsonTranscoding();
// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo { Title = "gRPC transcoding", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
app.MapGrpcService<ToDoService>();
// app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
