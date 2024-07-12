using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApiHealthWave.Context;
using HospitalCore_core.Services;
using HospitalCore_core.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
// Crear variable para la cadena de conexión 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Registrar servicio para la conexión 
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// Configurar HttpClient para los servicios del Core
builder.Services.AddHttpClient<IUsuarioService, UsuarioService>();
builder.Services.AddHttpClient<IAfeccionService, AfeccionService>();
builder.Services.AddHttpClient<IConsultaService, ConsultaService>();
builder.Services.AddHttpClient<IAseguradoraService, AseguradoraService>();
builder.Services.AddHttpClient<IAutorizacionService, AutorizacionService>();
builder.Services.AddHttpClient<IConsultorioService, ConsultorioService>();
builder.Services.AddHttpClient<ICuentaCobrarService, CuentaCobrarService>();
builder.Services.AddHttpClient<IFacturaService, FacturaService>();
builder.Services.AddHttpClient<IIngresoService, IngresoService>();
builder.Services.AddHttpClient<IMetodoDePagoService, MetodoDePagoService>();
builder.Services.AddHttpClient<IPagoService, PagoService>();
builder.Services.AddHttpClient<IProductoService, ProductoService>();
builder.Services.AddHttpClient<ISalaService, SalaService>();
builder.Services.AddHttpClient<IServicioService, ServicioService>();
builder.Services.AddHttpClient<ITipoServicioService, TipoServicioService>();

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
