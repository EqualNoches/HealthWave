using Microsoft.EntityFrameworkCore;
using WebApiHealthWave.Context;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Crear variable para la cadena de conexión 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//Registrar servicio para la conexión 
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiHealthWave", Version = "v1" });
    c.DocumentFilter<HideSchemaDocumentFilter>();
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

// Clase para ocultar esquemas específicos en Swagger
public class HideSchemaDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var schemasToRemove = new List<string> { "SalaDto" };
        foreach (var schema in schemasToRemove)
        {
            if (swaggerDoc.Components.Schemas.ContainsKey(schema))
            {
                swaggerDoc.Components.Schemas.Remove(schema);
            }
        }
    }
}
