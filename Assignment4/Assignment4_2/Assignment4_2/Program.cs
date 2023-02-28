using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver.Linq;
using System.Collections;
using Assignment4_2.Models;
using Assignment4_2.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddSingleton<CardsService>();
builder.Services.AddSingleton<CardtypesService>();
builder.Services.AddSingleton<ClassesService>();
builder.Services.AddSingleton<RaritiesService>();
builder.Services.AddSingleton<SetsService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<HearthstoneDatabaseSettings>(builder.Configuration.GetSection("Assignment4Database"));
builder.Services.AddAutoMapper(typeof(Program));

//Adding logging
builder.Host.UseSerilog((context, services, configuration) => {
    configuration.ReadFrom.Configuration(context.Configuration).Enrich.FromLogContext()
    .WriteTo.Console();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();