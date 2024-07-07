using System;
using System.Collections.Generic;
using HospitalCore_core.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalCore_core.Context;

public partial class HospitalCore : DbContext
{
    public HospitalCore()
    {
    }

    public HospitalCore(DbContextOptions<HospitalCore> options)
        : base(options)
    {
    }

    public virtual DbSet<Afeccion> Afeccions { get; set; }

    public virtual DbSet<Aseguradora> Aseguradoras { get; set; }

    public virtual DbSet<Autorizacion> Autorizacions { get; set; }

    public virtual DbSet<Consultorio> Consultorios { get; set; }

    public virtual DbSet<Consultum> Consulta { get; set; }

    public virtual DbSet<CuentaCobrar> CuentaCobrars { get; set; }

    
    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FacturaProducto> FacturaProductos { get; set; }

    public virtual DbSet<FacturaServicio> FacturaServicios { get; set; }

    public virtual DbSet<Ingreso> Ingresos { get; set; }

    public  virtual DbSet<MetodoDePago> MetodosDePago { get; set; }

    public virtual DbSet<Pago?> Pagos { get; set; }

    public virtual DbSet<PerfilUsuario> PerfilUsuarios { get; set; }

    public virtual DbSet<PrescripcionProducto> PrescripcionProductos { get; set; }

    public virtual DbSet<Producto?> Productos { get; set; }
    

    public virtual DbSet<Sala> Salas { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }
    
    public virtual DbSet<Usuario> Usuarios { get; set; }
    
    
    public virtual DbSet<TipoServicio> TipoServicios { get; set; }

    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:corehospital.database.windows.net,1433;Initial Catalog=CareSys;Persist Security Info=False;User ID=core;Password=Hospital1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Afeccion>(entity =>
        {
            entity.HasKey(e => e.IdAfeccion).HasName("PK__Afeccion__EE1794EB570F07E8");

            entity.ToTable("Afeccion");

            entity.Property(e => e.IdAfeccion).HasColumnName("IDAfeccion");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasMany(d => d.Ingresos).WithMany(p => p.Afecciones)
                .UsingEntity<Dictionary<string, object>>(
                    "IngresoAfeccion",
                    r => r.HasOne<Ingreso>().WithMany()
                        .HasForeignKey("Idingreso")
                        .HasConstraintName("FK__IngresoAf__IDIng__74643BF9"),
                    l => l.HasOne<Afeccion>().WithMany()
                        .HasForeignKey("Idafeccion")
                        .HasConstraintName("FK__IngresoAf__IDAfe__737017C0"),
                    j =>
                    {
                        j.HasKey("Idafeccion", "Idingreso").HasName("PK__IngresoA__0C5302350527F7C2");
                        j.ToTable("IngresoAfeccion");
                        j.IndexerProperty<int>("Idafeccion").HasColumnName("IDAfeccion");
                        j.IndexerProperty<int>("Idingreso").HasColumnName("IDIngreso");
                    });
        });


        modelBuilder.Entity<Aseguradora>(entity =>
        {
            entity.HasKey(e => e.Idaseguradora).HasName("PK__Asegurad__20F4075168EE4388");

            entity.ToTable("Aseguradora");

            entity.Property(e => e.Idaseguradora).HasColumnName("IDAseguradora");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Dirección)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Autorizacion>(entity =>
        {
            entity.HasKey(e => e.Idautorizacion).HasName("PK__Autoriza__219BB3109759BF30");

            entity.ToTable("Autorizacion");

            entity.Property(e => e.Idautorizacion).HasColumnName("IDAutorizacion");
            entity.Property(e => e.Idaseguradora).HasColumnName("IDAseguradora");
            entity.Property(e => e.MontoAutorizado)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdaseguradoraNavigation).WithMany(p => p.Autorizacions)
                .HasForeignKey(d => d.Idaseguradora)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Autorizac__IDAse__3B2BBE9D");
        });

        modelBuilder.Entity<Consultorio>(entity =>
        {
            entity.HasKey(e => e.IDConsultorio).HasName("PK__Consulto__7F4AA627971A8B57");

            entity.ToTable("Consultorio");

            entity.Property(e => e.IDConsultorio).HasColumnName("IDConsultorio");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Consultum>(entity =>
        {
            entity.HasKey(e => e.ConsultaCodigo).HasName("PK__Consulta__13F4EB72487E3A04");

            entity.Property(e => e.ConsultaCodigo).ValueGeneratedNever();
            entity.Property(e => e.CodigoDocumentoMedico)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CodigoPaciente)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Costo)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Idautorizacion).HasColumnName("IDAutorizacion");
            entity.Property(e => e.Idconsultorio).HasColumnName("IDConsultorio");
            entity.Property(e => e.Motivo)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.CodigoPacienteNavigation).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.CodigoPaciente)
                .HasConstraintName("FK__Consulta__Codigo__511AFFBC");

            entity.HasOne(d => d.IdautorizacionNavigation).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.Idautorizacion)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Consulta__IDAuto__5303482E");

            entity.HasOne(d => d.IdconsultorioNavigation).WithMany(p => p.ConsultaCodigo)
                .HasForeignKey(d => d.Idconsultorio)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Consulta__IDCons__520F23F5");

            entity.HasMany(d => d.Idafeccions).WithMany(p => p.ConsultaCodigo)
                .UsingEntity<Dictionary<string, object>>(
                    "ConsultaAfeccion",
                    r => r.HasOne<Afeccion>().WithMany()
                        .HasForeignKey("Idafeccion")
                        .HasConstraintName("FK__ConsultaA__IDAfe__7093AB15"),
                    l => l.HasOne<Consultum>().WithMany()
                        .HasForeignKey("ConsultaCodigo")
                        .HasConstraintName("FK__ConsultaA__Consu__6F9F86DC"),
                    j =>
                    {
                        j.HasKey("ConsultaCodigo", "Idafeccion").HasName("PK__Consulta__BD15923C39006600");
                        j.ToTable("ConsultaAfeccion");
                        j.IndexerProperty<int>("Idafeccion").HasColumnName("IDAfeccion");
                    });

            entity.HasMany(d => d.ServicioCodigos).WithMany(p => p.ConsultaCodigos)
                .UsingEntity<Dictionary<string, object>>(
                    "ConsultaServicio",
                    r => r.HasOne<Servicio>().WithMany()
                        .HasForeignKey("ServicioCodigo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ConsultaS__Servi__7C055DC1"),
                    l => l.HasOne<Consultum>().WithMany()
                        .HasForeignKey("ConsultaCodigo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ConsultaS__Consu__7B113988"),
                    j =>
                    {
                        j.HasKey("ConsultaCodigo", "ServicioCodigo").HasName("PK__Consulta__816AD288DF72C696");
                        j.ToTable("ConsultaServicio");
                    });
        });

  modelBuilder.Entity<CuentaCobrar>(entity =>
        {
            entity.HasKey(e => e.Idcuenta).HasName("PK__CuentaCo__F016C5A14495BBB3");

            entity.ToTable("CuentaCobrar");

            entity.Property(e => e.Idcuenta).HasColumnName("IDCuenta");
            entity.Property(e => e.Balance)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CodigoPaciente)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.CodigoPacienteNavigation).WithMany(p => p.CuentaCobrars)
                .HasForeignKey(d => d.CodigoPaciente)
                .HasConstraintName("FK__CuentaCob__Codig__4D4A6ED8");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioCodigo).HasName("PRIMARY");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.DocumentoUsuario, "documentoUsuario").IsUnique();

            entity.Property(e => e.UsuarioCodigo)
                .HasMaxLength(30)
                .HasColumnName("usuarioCodigo");

            entity.Property(e => e.DocumentoUsuario)
                .HasMaxLength(30)
                .HasColumnName("documentoUsuario");

            entity.Property(e => e.UsuarioContra)
                .HasMaxLength(255)
                .HasColumnName("usuarioContra");

            entity.HasOne(d => d.PerfilUsuario).WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(d => d.DocumentoUsuario)
                .HasConstraintName("Usuario_ibfk_1");
        });




        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.FacturaCodigo).HasName("PK__Factura__3E7D71721E0E4213");

            entity.ToTable("Factura");

            entity.Property(e => e.FacturaCodigo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CodigoPaciente)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Idcuenta).HasColumnName("IDCuenta");
            entity.Property(e => e.Idingreso).HasColumnName("IDIngreso");
            entity.Property(e => e.MontoSubtotal)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MontoTotal)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Rnc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RNC");

            entity.HasOne(d => d.CodigoMetodoDePagoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.CodigoMetodoDePago)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Factura__CodigoM__5F691F13");

            entity.HasOne(d => d.CodigoPacienteNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.CodigoPaciente)
                .HasConstraintName("FK__Factura__CodigoP__605D434C");

            entity.HasOne(d => d.ConsultaCodigoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.ConsultaCodigo)
                .HasConstraintName("FK__Factura__Consult__6339AFF7");

            entity.HasOne(d => d.IdcuentaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.Idcuenta)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Factura__IDCuent__62458BBE");

            entity.HasOne(d => d.IdingresoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.Idingreso)
                .HasConstraintName("FK__Factura__IDIngre__61516785");
        });

        modelBuilder.Entity<FacturaProducto>(entity =>
        {
            entity.HasKey(e => new { e.FacturaCodigoProducto, e.Idproducto }).HasName("PK__FacturaP__64C0DE590F3AD90C");

            entity.ToTable("FacturaProducto");

            entity.Property(e => e.FacturaCodigoProducto)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Idproducto).HasColumnName("IDProducto");
            entity.Property(e => e.Idautorizacion).HasColumnName("IDAutorizacion");
            entity.Property(e => e.Precio)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.FacturaCodigoNavigation).WithMany(p => p.FacturaProductos)
                .HasForeignKey(d => d.FacturaCodigoProducto)
                .HasConstraintName("FK__FacturaPr__Factu__7FD5EEA5");

            entity.HasOne(d => d.IdautorizacionNavigation).WithMany(p => p.FacturaProductos)
                .HasForeignKey(d => d.Idautorizacion)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__FacturaPr__IDAut__01BE3717");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.FacturaProductos)
                .HasForeignKey(d => d.Idproducto)
                .HasConstraintName("FK__FacturaPr__IDPro__00CA12DE");
        });

        modelBuilder.Entity<FacturaServicio>(entity =>
        {
            entity.HasKey(e => new { e.FacturaCodigoServicio, e.Idproducto }).HasName("PK__FacturaS__64C0DE5954B74F27");

            entity.ToTable("FacturaServicio");

            entity.Property(e => e.FacturaCodigoServicio)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Idproducto).HasColumnName("IDProducto");
            entity.Property(e => e.Costo)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Idautorizacion).HasColumnName("IDAutorizacion");
            entity.Property(e => e.ServicioCodigo)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.FacturaCodigoNavigation).WithMany(p => p.FacturaServicios)
                .HasForeignKey(d => d.FacturaCodigoServicio)
                .HasConstraintName("FK__FacturaSe__Factu__670A40DB");

            entity.HasOne(d => d.IdautorizacionNavigation).WithMany(p => p.FacturaServicios)
                .HasForeignKey(d => d.Idautorizacion)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__FacturaSe__IDAut__68F2894D");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.FacturaServicios)
                .HasForeignKey(d => d.Idproducto)
                .HasConstraintName("FK__FacturaSe__IDPro__67FE6514");
        });

        modelBuilder.Entity<Ingreso>(entity =>
        {
            entity.HasKey(e => e.IDIngreso).HasName("PK__Ingreso__24496DEF2B4F4391");

            entity.ToTable("Ingreso");

            entity.Property(e => e.IDIngreso).HasColumnName("IDIngreso");
            entity.Property(e => e.CodigoDocumentoMedico)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CodigoPaciente)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CostoEstancia)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Idautorizacion).HasColumnName("IDAutorizacion");

            entity.HasOne(d => d.CodigoDocumentoMedicoNavigation).WithMany(p => p.IngresoCodigoDocumentoMedicoNavigations)
                .HasForeignKey(d => d.CodigoDocumentoMedico)
                .HasConstraintName("FK__Ingreso__CodigoD__58BC2184");

            entity.HasOne(d => d.CodigoPacienteNavigation).WithMany(p => p.IngresoCodigoPacienteNavigations)
                .HasForeignKey(d => d.CodigoPaciente)
                .HasConstraintName("FK__Ingreso__CodigoP__57C7FD4B");

            entity.HasOne(d => d.ConsultaCodigoNavigation).WithMany(p => p.Ingresos)
                .HasForeignKey(d => d.ConsultaCodigo)
                .HasConstraintName("FK__Ingreso__Consult__59B045BD");

            entity.HasOne(d => d.IdautorizacionNavigation).WithMany(p => p.Ingresos)
                .HasForeignKey(d => d.Idautorizacion)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Ingreso__IDAutor__5AA469F6");

            entity.HasOne(d => d.NumSalaNavigation).WithMany(p => p.Ingresos)
                .HasForeignKey(d => d.NumSala)
                .HasConstraintName("FK__Ingreso__NumSala__56D3D912");
        });

        modelBuilder.Entity<MetodoDePago>(entity =>
        {
            entity.HasKey(e => e.CodigoMetodoDePago).HasName("PK__MetodoDe__ABC0F37CD9F0E16F");
            entity.ToTable("MetodoDePago");
            entity.Property(e => e.CodigoMetodoDePago).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });


        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Idpago).HasName("PK__Pago__8A5C3DEE2874F465");

            entity.ToTable("Pago");

            entity.Property(e => e.Idpago).HasColumnName("IDPago");
            entity.Property(e => e.Idcuenta).HasColumnName("IDCuenta");
            entity.Property(e => e.MontoPagado)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdcuentaNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.Idcuenta)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Pago__IDCuenta__058EC7FB");
        });

        modelBuilder.Entity<PerfilUsuario>(entity =>
        {
            entity.HasKey(e => e.CodigoDocumento).HasName("PRIMARY");

            entity.ToTable("PerfilUsuario");

            entity.HasIndex(e => e.NumLicenciaMedica, "numLicenciaMedica").IsUnique();

            entity.Property(e => e.CodigoDocumento)
                .HasMaxLength(30)
                .HasColumnName("CodigoDocumento");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("Apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .HasColumnName("Correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("Direccion");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("FechaNacimiento");
            entity.Property(e => e.Genero)
                .HasColumnType("enum('M','F')")
                .HasColumnName("Genero");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("Nombre");
            entity.Property(e => e.NumLicenciaMedica).HasColumnName("NumLicenciaMedica");
            entity.Property(e => e.Rol)
                .HasColumnType("enum('P','A','M','E','C')")
                .HasColumnName("Rol");
            entity.Property(e => e.Telefono)
                .HasMaxLength(18)
                .HasColumnName("Telefono");
            entity.Property(e => e.TipoDocumento)
                .HasDefaultValueSql("'I'")
                .HasColumnType("enum('I','P')")
                .HasColumnName("TipoDocumento");
        });




        modelBuilder.Entity<PrescripcionProducto>(entity =>
        {
            entity.HasKey(e => new { e.Idproducto, e.ConsultaCodigo }).HasName("PK__Prescrip__9AE5BC03105D8B7C");

            entity.ToTable("PrescripcionProducto");

            entity.Property(e => e.Idproducto).HasColumnName("IDProducto");

            entity.HasOne(d => d.ConsultaCodigoNavigation).WithMany(p => p.PrescripcionProductos)
                .HasForeignKey(d => d.ConsultaCodigo)
                .HasConstraintName("FK__Prescripc__Consu__6CC31A31");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.PrescripcionProductos)
                .HasForeignKey(d => d.Idproducto)
                .HasConstraintName("FK__Prescripc__IDPro__6BCEF5F8");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Idproducto).HasName("PK__Producto__ABDAF2B4FE680204");

            entity.ToTable("Producto");

            entity.Property(e => e.Idproducto).HasColumnName("IDProducto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Precio)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");
        });
        
        

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.HasKey(e => e.NumSala).HasName("PK__Sala__C5175B576D37E036");

            entity.ToTable("Sala");

            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.ServicioCodigo).HasName("PK__Servicio__29E39FA8E33022DE");

            entity.ToTable("Servicio");

            entity.Property(e => e.ServicioCodigo).ValueGeneratedNever();
            entity.Property(e => e.Costo)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IDAseguradora).HasColumnName("IDAseguradora");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IDTipoServicio).HasColumnName("IDTipoServicio");

            entity.HasOne(d => d.Aseguradora).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.IDAseguradora)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Servicio__IDAseg__3FF073BA");

            entity.HasOne(d => d.TipoServicio).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.IDTipoServicio)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Servicio__IDTipo__3EFC4F81");

            entity.HasMany(d => d.CodigoPacientes).WithMany(p => p.ServicioCodigos)
                .UsingEntity<Dictionary<string, object>>(
                    "ReservaServicio",
                    r => r.HasOne<PerfilUsuario>().WithMany()
                        .HasForeignKey("CodigoPaciente")
                        .HasConstraintName("FK__ReservaSe__Codig__7834CCDD"),
                    l => l.HasOne<Servicio>().WithMany()
                        .HasForeignKey("ServicioCodigo")
                        .HasConstraintName("FK__ReservaSe__Servi__7740A8A4"),
                    j =>
                    {
                        j.HasKey("ServicioCodigo", "CodigoPaciente").HasName("PK__ReservaS__5BE731C725A07910");
                        j.ToTable("ReservaServicio");
                        j.IndexerProperty<string>("CodigoPaciente")
                            .HasMaxLength(30)
                            .IsUnicode(false);
                    });
        });

        modelBuilder.Entity<TipoServicio>(entity =>
        {
            entity.HasKey(e => e.IdTipoServicio).HasName("PRIMARY");

            entity.ToTable("TipoServicio");

            entity.Property(e => e.IdTipoServicio).HasColumnName("idTipoServicio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}