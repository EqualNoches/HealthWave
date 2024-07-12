using Microsoft.EntityFrameworkCore;
using WebApiHealthWave.Models;

namespace WebApiHealthWave.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Usuario> Usuarios { get; set; }  
        public DbSet<PerfilUsuario> PerfilUsuarios { get; set; }
        public DbSet<TipoServicio> TipoServicios { get; set; }
        public DbSet<Aseguradora> Aseguradoras { get; set; }
        public DbSet<Consultorio> Consultorios { get; set; }
        public DbSet<Autorizacion> Autorizaciones { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<MetodoDePago> MetodosDePago { get; set; }
        public DbSet<Afeccion> Afecciones { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<CuentaCobrar> CuentasCobrar { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Ingreso> Ingresos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<FacturaServicio> FacturaServicios { get; set; }
        public DbSet<PrescripcionProducto> PrescripcionProductos { get; set; }
        public DbSet<ConsultaAfeccion> ConsultaAfecciones { get; set; }
        public DbSet<IngresoAfeccion> IngresoAfecciones { get; set; }
        public DbSet<ReservaServicio> ReservaServicios { get; set; }
        public DbSet<ConsultaServicio> ConsultaServicios { get; set; }
        public DbSet<FacturaProducto> FacturaProductos { get; set; }
        public DbSet<Pago> Pagos { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuarioCodigo);
                entity.Property(e => e.UsuarioCodigo).IsRequired().HasMaxLength(30);
                entity.Property(e => e.DocumentoUsuario).IsRequired().HasMaxLength(30);
                entity.Property(e => e.UsuarioContra).IsRequired().HasMaxLength(255);

                entity.HasIndex(e => e.DocumentoUsuario).IsUnique();

                entity.HasOne(e => e.PerfilUsuario)
                      .WithMany(p => p.Usuarios)
                      .HasForeignKey(e => e.DocumentoUsuario)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PerfilUsuario>()
    .HasKey(p => p.CodigoDocumento);

            modelBuilder.Entity<PerfilUsuario>()
                .Property(p => p.CodigoDocumento)
                .HasMaxLength(30);  

            modelBuilder.Entity<PerfilUsuario>()
                .Property(p => p.TipoDocumento)
                .HasDefaultValue('I');

            modelBuilder.Entity<PerfilUsuario>()
                .Property(p => p.Género)
                .HasConversion<string>()
                .HasMaxLength(1);

            modelBuilder.Entity<PerfilUsuario>()
                .Property(p => p.Rol)
                .HasConversion<string>()
                .HasMaxLength(1);

            modelBuilder.Entity<PerfilUsuario>()
                .HasMany(p => p.Usuarios)
                .WithOne(c => c.PerfilUsuario)
                .HasForeignKey(u => u.DocumentoUsuario)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<TipoServicio>()
                .HasKey(ts => ts.TipoServicioId);

            modelBuilder.Entity<TipoServicio>()
                .Property(ts => ts.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Aseguradora>()
                .HasKey(a => a.IDAseguradora);

            modelBuilder.Entity<Aseguradora>()
                .Property(a => a.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Aseguradora>()
                .Property(a => a.Dirección)
                .HasMaxLength(200);

            modelBuilder.Entity<Aseguradora>()
                .Property(a => a.Teléfono)
                .HasMaxLength(20);

            modelBuilder.Entity<Aseguradora>()
                .Property(a => a.Correo)
                .HasMaxLength(100);

            modelBuilder.Entity<Consultorio>()
                .HasKey(c => c.IDConsultorio);

            modelBuilder.Entity<Consultorio>()
                .Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Consultorio>()
                .Property(c => c.Dirección)
                .HasMaxLength(200);

            modelBuilder.Entity<Consultorio>()
                .Property(c => c.Teléfono)
                .HasMaxLength(20);

            modelBuilder.Entity<Autorizacion>()
                .HasKey(a => a.IDAutorizacion);

            modelBuilder.Entity<Autorizacion>()
                .Property(a => a.FechaAutorizacion)
                .IsRequired();

            modelBuilder.Entity<Autorizacion>()
                .Property(a => a.MontoAutorizado)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<Autorizacion>()
                .HasOne(a => a.Aseguradora)
                .WithMany()
                .HasForeignKey(a => a.IDAseguradora)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Servicio>()
                .HasKey(s => s.ServicioCodigo);

            modelBuilder.Entity<Servicio>()
                .Property(s => s.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Servicio>()
                .Property(s => s.Descripción)
                .HasMaxLength(200);

            modelBuilder.Entity<Servicio>()
                .Property(s => s.Costo)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.TipoServicioNavigation)
                .WithMany()
                .HasForeignKey(s => s.TipoServicio)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Servicio>()
                .HasOne(s => s.Aseguradora)
                .WithMany()
                .HasForeignKey(s => s.IDAseguradora)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Producto>()
                .HasKey(p => p.IDProducto);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Descripción)
                .HasMaxLength(200);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<MetodoDePago>()
                .HasKey(m => m.CodigoMetodoDePago);

            modelBuilder.Entity<MetodoDePago>()
                .Property(m => m.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Afeccion>()
                .HasKey(a => a.IDAfeccion);

            modelBuilder.Entity<Afeccion>()
                .Property(a => a.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Afeccion>()
                .Property(a => a.Descripción)
                .HasMaxLength(200);

            modelBuilder.Entity<Sala>()
                .HasKey(s => s.NumSala);

            modelBuilder.Entity<Sala>()
                .Property(s => s.Estado)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<CuentaCobrar>()
                .HasKey(cc => cc.IDCuenta);

            modelBuilder.Entity<CuentaCobrar>()
                .Property(cc => cc.Balance)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<CuentaCobrar>()
                .Property(cc => cc.Estado)
                .IsRequired()
                .HasMaxLength(1);

            modelBuilder.Entity<CuentaCobrar>()
                .HasOne(cc => cc.PerfilUsuario)
                .WithMany(p => p.CuentasCobrar)
                .HasForeignKey(cc => cc.CodigoPaciente)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Consulta>()
                .HasKey(c => c.ConsultaCodigo);

            modelBuilder.Entity<Consulta>()
                .Property(c => c.Fecha)
                .IsRequired();

            modelBuilder.Entity<Consulta>()
                .Property(c => c.Estado)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Consulta>()
                .Property(c => c.Costo)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<Consulta>()
                .Property(c => c.Motivo)
                .HasMaxLength(200);

            modelBuilder.Entity<Consulta>()
                .Property(c => c.Descripcion)
                .HasMaxLength(200);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Paciente)
                .WithMany(p => p.Consultas)
                .HasForeignKey(c => c.CodigoPaciente)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Consultorio)
                .WithMany()
                .HasForeignKey(c => c.IDConsultorio)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Consulta>()
                .HasOne(c => c.Autorizacion)
                .WithMany()
                .HasForeignKey(c => c.IDAutorizacion)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ingreso>()
                .HasKey(i => i.IDIngreso);

            modelBuilder.Entity<Ingreso>()
                .Property(i => i.CostoEstancia)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<Ingreso>()
                .Property(i => i.FechaIngreso)
                .IsRequired();

            modelBuilder.Entity<Ingreso>()
                .HasOne(i => i.Sala)
                .WithMany(s => s.Ingresos)
                .HasForeignKey(i => i.NumSala)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ingreso>()
                .HasOne(i => i.Paciente)
                .WithMany(p => p.IngresosPaciente)
                .HasForeignKey(i => i.CodigoPaciente)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ingreso>()
                .HasOne(i => i.Medico)
                .WithMany(m => m.IngresosMedico)
                .HasForeignKey(i => i.CodigoDocumentoMedico)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ingreso>()
                .HasOne(i => i.Consulta)
                .WithMany(c => c.Ingresos)
                .HasForeignKey(i => i.ConsultaCodigo)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ingreso>()
                .HasOne(i => i.Autorizacion)
                .WithMany(a => a.Ingresos)
                .HasForeignKey(i => i.IDAutorizacion)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Factura>()
                .HasKey(f => f.FacturaCodigo);

            modelBuilder.Entity<Factura>()
                .Property(f => f.MontoTotal)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<Factura>()
                .Property(f => f.MontoSubtotal)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<Factura>()
                .Property(f => f.Fecha)
                .IsRequired();

            modelBuilder.Entity<Factura>()
                .Property(f => f.RNC)
                .HasMaxLength(50);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.MetodoDePago)
                .WithMany(m => m.Facturas)
                .HasForeignKey(f => f.CodigoMetodoDePago)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Paciente)
                .WithMany(p => p.Facturas)
                .HasForeignKey(f => f.CodigoPaciente)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Ingreso)
                .WithMany(i => i.Facturas)
                .HasForeignKey(f => f.IDIngreso)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Cuenta)
                .WithMany(c => c.Facturas)
                .HasForeignKey(f => f.IDCuenta)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Consulta)
                .WithMany(c => c.Facturas)
                .HasForeignKey(f => f.ConsultaCodigo)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FacturaServicio>()
                .HasKey(fs => new { fs.FacturaCodigoServicio, fs.IDProducto });

            modelBuilder.Entity<FacturaServicio>()
                .Property(fs => fs.Costo)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<FacturaServicio>()
                .HasOne(fs => fs.Factura)
                .WithMany(f => f.FacturaServicios)
                .HasForeignKey(fs => fs.FacturaCodigoServicio)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FacturaServicio>()
                .HasOne(fs => fs.Producto)
                .WithMany(p => p.FacturaServicios)
                .HasForeignKey(fs => fs.IDProducto)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FacturaServicio>()
                .HasOne(fs => fs.Autorizacion)
                .WithMany(a => a.FacturaServicios)
                .HasForeignKey(fs => fs.IDAutorizacion)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PrescripcionProducto>()
                .HasKey(pp => new { pp.IDProducto, pp.ConsultaCodigo });

            modelBuilder.Entity<PrescripcionProducto>()
                .HasOne(pp => pp.Producto)
                .WithMany(p => p.PrescripcionProductos)
                .HasForeignKey(pp => pp.IDProducto)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PrescripcionProducto>()
                .HasOne(pp => pp.Consulta)
                .WithMany(c => c.PrescripcionProductos)
                .HasForeignKey(pp => pp.ConsultaCodigo)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ConsultaAfeccion>()
                .HasKey(ca => new { ca.ConsultaCodigo, ca.IDAfeccion });

            modelBuilder.Entity<ConsultaAfeccion>()
                .HasOne(ca => ca.Consulta)
                .WithMany(c => c.ConsultaAfecciones)
                .HasForeignKey(ca => ca.ConsultaCodigo)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ConsultaAfeccion>()
                .HasOne(ca => ca.Afeccion)
                .WithMany(a => a.ConsultaAfecciones)
                .HasForeignKey(ca => ca.IDAfeccion)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IngresoAfeccion>()
                .HasKey(ia => new { ia.IDAfeccion, ia.IDIngreso });

            modelBuilder.Entity<IngresoAfeccion>()
                .HasOne(ia => ia.Afeccion)
                .WithMany(a => a.IngresoAfecciones)
                .HasForeignKey(ia => ia.IDAfeccion)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IngresoAfeccion>()
                .HasOne(ia => ia.Ingreso)
                .WithMany(i => i.IngresoAfecciones)
                .HasForeignKey(ia => ia.IDIngreso)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ReservaServicio>()
                .HasKey(rs => new { rs.ServicioCodigo, rs.CodigoPaciente });

            modelBuilder.Entity<ReservaServicio>()
                .HasOne(rs => rs.Servicio)
                .WithMany(s => s.ReservaServicios)
                .HasForeignKey(rs => rs.ServicioCodigo)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReservaServicio>()
                .HasOne(rs => rs.Paciente)
                .WithMany(p => p.ReservaServicios)
                .HasForeignKey(rs => rs.CodigoPaciente)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ConsultaServicio>()
                .HasKey(cs => new { cs.ConsultaCodigo, cs.ServicioCodigo });

            modelBuilder.Entity<ConsultaServicio>()
                .HasOne(cs => cs.Consulta)
                .WithMany(c => c.ConsultaServicios)
                .HasForeignKey(cs => cs.ConsultaCodigo)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ConsultaServicio>()
                .HasOne(cs => cs.Servicio)
                .WithMany(s => s.ConsultaServicios)
                .HasForeignKey(cs => cs.ServicioCodigo)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<FacturaProducto>()
                .HasKey(fp => new { fp.FacturaCodigoProducto, fp.IDProducto });

            modelBuilder.Entity<FacturaProducto>()
                .Property(fp => fp.Precio)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<FacturaProducto>()
                .HasOne(fp => fp.Factura)
                .WithMany(f => f.FacturaProductos)
                .HasForeignKey(fp => fp.FacturaCodigoProducto)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FacturaProducto>()
                .HasOne(fp => fp.Producto)
                .WithMany(p => p.FacturaProductos)
                .HasForeignKey(fp => fp.IDProducto)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FacturaProducto>()
                .HasOne(fp => fp.Autorizacion)
                .WithMany(a => a.FacturaProductos)
                .HasForeignKey(fp => fp.IDAutorizacion)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pago>()
                .HasKey(p => p.IDPago);

            modelBuilder.Entity<Pago>()
                .Property(p => p.MontoPagado)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<Pago>()
                .Property(p => p.Fecha)
                .IsRequired();

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Cuenta)
                .WithMany(c => c.Pagos)
                .HasForeignKey(p => p.IDCuenta)
                .OnDelete(DeleteBehavior.Cascade);


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }
}