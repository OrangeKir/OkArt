using System.Data;
using MediatR;
using Npgsql;
using OkArt.Infrastructure.Helpers;
using OkArt.Integrations;

var builder = WebApplication.CreateBuilder(args);
var type = typeof(Program);
var dbConnectionString = builder.Configuration.GetConnectionString("DbConnectionString");
var fsConnectionString = builder.Configuration.GetConnectionString("FsConnectionString");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.CustomSchemaIds(t => t.ToString());
});
builder.Services.AddMediatR(type);
builder.Services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(dbConnectionString));
builder.Services.AddScoped<IFsContext>(_ => new FsContext(fsConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

MigrationsHelper.Migrate(dbConnectionString);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();