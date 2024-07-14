using Microsoft.EntityFrameworkCore;


using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApiHealthWave.Context;
using WebApiHealthWave.Services.Interfaces;
using WebApiHealthWave.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.IO;
using NLog;
using NLog.Web;
using WebApiHealthWave.Services.Firestore;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    logger.Debug("init main");

    var builder = WebApplication.CreateBuilder(args);

    // Initialize NLog
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    // Initialize Firebase
   

    // Add services to the container.

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

    // Add CORS policy
    // Add CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowCoreApp",
            builder =>
            {
                builder.WithOrigins("https://localhost:7181") // URL del Core
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });



    // Register HttpClient for each service
    builder.Services.AddHttpClient<IAfeccionService, AfeccionService>();
    builder.Services.AddHttpClient<IAseguradoraService, AseguradoraService>();
    builder.Services.AddHttpClient<IAutorizacionService, AutorizacionService>();
    builder.Services.AddHttpClient<IConsultorioService, ConsultorioService>();
    builder.Services.AddHttpClient<ICuentaCobrarService, CuentaCobrarService>();
    builder.Services.AddHttpClient<IFacturaService, FacturaService>();
    builder.Services.AddHttpClient<IIngresoService, IngresoService>();
    builder.Services.AddHttpClient<IMetodoPagoService, MetodoPagoService>();
    builder.Services.AddHttpClient<IPagoService, PagoService>();
    builder.Services.AddHttpClient<IProductoService, ProductoService>();
    builder.Services.AddHttpClient<IReservaServicio, ReservaServicioService>();
    builder.Services.AddHttpClient<ISalaService, SalaService>();
    builder.Services.AddHttpClient<IServicioService, ServicioService>();
    builder.Services.AddHttpClient<ITipoServicioService, TipoServicioService>();
    builder.Services.AddHttpClient<IUsuarioService, UsuarioService>();

    builder.Services.AddScoped<FirestoreService>();

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Integracion_HealthWave", Version = "v1" });
    });

    var app = builder.Build();



    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();


    app.UseCors("AllowCoreApp");


    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}
