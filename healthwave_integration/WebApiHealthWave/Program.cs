using Microsoft.EntityFrameworkCore;

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApiHealthWave.Context;
using WebApiHealthWave.Services.Interfaces;
using WebApiHealthWave.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddOpenApiDocument();


// Add services to the container.
// Crear variable para la cadena de conexión 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Registrar servicio para la conexión 
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IAfeccionService, AfeccionService>();
builder.Services.AddScoped<IAseguradoraService, AseguradoraService>();
builder.Services.AddScoped<IAutorizacionService, AutorizacionService>();
builder.Services.AddScoped<IConsultorioService, ConsultorioService>();
builder.Services.AddScoped<ICuentaCobrarService, CuentaCobrarService>();
builder.Services.AddScoped<IFacturaService, FacturaService>();
builder.Services.AddScoped<IIngresoService, IngresoService>();
builder.Services.AddScoped<IMetodoPagoService, MetodoPagoService>();
builder.Services.AddScoped<IPagoService, PagoService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IReservaServicio, ReservaServicioService>();
builder.Services.AddScoped<ISalaService, SalaService>();
builder.Services.AddScoped<IServicioService, ServicioService>();
builder.Services.AddScoped<ITipoServicioService, TipoServicioService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

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

app.UseCors("AllowCoreApp"); // Aplicar la política CORS

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
