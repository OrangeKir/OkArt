using System.Data;
using MediatR;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var type = typeof(Program);
var dbConnectionString = builder.Configuration.GetConnectionString("DbConnectionString");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(type);
builder.Services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(dbConnectionString));

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