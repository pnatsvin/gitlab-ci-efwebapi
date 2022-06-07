using Microsoft.EntityFrameworkCore;
using EFWebApi_v2.Data;
using EFWebapi_v2.Models;
using EFWebapi_v2.Logging;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

 // Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SqlEfContex>(opt =>
{
var config = builder.Configuration;

 var server = config["DbServer"] ?? "51.250.94.14";
 var port = config["DbPort"] ?? "1433";
 var user = config["DbUser"] ?? "sa";
 var password = config["DbPassword"] ?? "Password011090";

 var connectionString = $"Server={server},{port};Initial Catalog=RecordsDb;User ID={user};Password={password}";

opt.UseSqlServer(connectionString);
 });

builder.Logging.ClearProviders()
      .AddDbLogger(configure => { });

builder.PopulateDb();



var app = builder.Build();

 // Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
 {
     app.UseSwagger();
     app.UseSwaggerUI();
 }


app.MapControllers();

app.Run();
