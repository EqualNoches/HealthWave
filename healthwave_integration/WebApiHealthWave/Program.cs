using Microsoft.EntityFrameworkCore;

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApiHealthWave.Context;
using HospitalCore_core.Services; // Importa los servicios del core
using HospitalCore_core.Services.Interfaces; // Importa las interfaces de servicios del core

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Crear variable para la cadena de conexión 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//Registrar servicio para la conexión 
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// Configurar HttpClient para los servicios del Core
builder.Services.AddHttpClient<IUsuarioService, UsuarioService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5042"); // URL del proyecto del core
});

builder.Services.AddHttpClient<IAfeccionService, AfeccionService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5042"); 
});

// Servicios
builder.Services.AddHttpClient<IConsultaService, ConsultaService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5042"); 
});

builder.Services.AddHttpClient<IAseguradoraService, AseguradoraService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5042"); 
});

builder.Services.AddHttpClient<IAutorizacionService, AutorizacionService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5042");
});

builder.Services.AddHttpClient<IConsultorioService, ConsultorioService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5042");
});

builder.Services.AddHttpClient<ICuentaCobrarService, CuentaCobrarService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5042");
});

builder.Services.AddHttpClient<IFacturaService, FacturaService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5042");
});

builder.Services.AddHttpClient<IIngresoService, IngresoService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5042");
});

builder.Services.AddHttpClient<IMetodoDePagoService, MetodoDePagoService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5042");
});

builder.Services.AddHttpClient<IPagoService, PagoService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5042");
});

builder.Services.AddHttpClient<IProductoService, ProductoService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5042");
});

builder.Services.AddHttpClient<ISalaService, SalaService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5042");
});

builder.Services.AddHttpClient<IServicioService, ServicioService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5042");
});

builder.Services.AddHttpClient<ITipoServicioService, TipoServicioService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5042");
});

builder.Services.AddHttpClient<IUsuarioService, UsuarioService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5042");
});



builder.Services.AddControllers();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCoreApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:5042") 
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

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

