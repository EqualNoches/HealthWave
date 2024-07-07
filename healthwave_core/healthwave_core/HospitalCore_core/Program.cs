using HospitalCore_core.Context;
using HospitalCore_core.Services;
using HospitalCore_core.Services.Interfaces;
using HospitalCore_core.Utilities;
using Microsoft.EntityFrameworkCore;

namespace HospitalCore_core;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        Configure(app, builder.Environment);

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("HospitalCoreDB");

        // Add controllers
        services.AddControllers();
        
        // Add DbContext
        services.AddDbContext<HospitalCore>(options =>
        {
            if (connectionString != null) options.UseSqlServer(connectionString);
            else throw new ArgumentException("The connection string is null.");
        });
        
        // Register services
        services.AddScoped<ITipoServicioService, TipoServicioService>();
        services.AddScoped<IAseguradoraService, AseguradoraService>();
        services.AddScoped<ICuentaCobrarService, CuentaCobrarService>();
        services.AddScoped<IProductoService, ProductoService>();
        services.AddScoped<IAutorizacionService, AutorizacionService>();
        services.AddScoped<IAfeccionService, AfeccionService>();
        services.AddScoped<IPagoService, PagoService>();
        services.AddScoped<IConsultorioService, ConsultorioService>();
        services.AddScoped<IMetodoDePagoService, MetodoDePagoService>();
        services.AddScoped<ISalaService, SalaService>();
        services.AddScoped<IServicioService, ServicioService>();
        services.AddScoped<IFacturaService, FacturaService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IConsultaService, ConsultaService>();
        services.AddScoped<IIngresoService, IngresoService>();

     


        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
    
    private static void Configure(WebApplication app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}
