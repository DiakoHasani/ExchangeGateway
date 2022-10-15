using EG.Repository.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
