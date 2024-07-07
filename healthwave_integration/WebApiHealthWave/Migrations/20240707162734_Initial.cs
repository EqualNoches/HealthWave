using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiHealthWave.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Afecciones",
                columns: table => new
                {
                    IDAfeccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripción = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Afecciones", x => x.IDAfeccion);
                });

            migrationBuilder.CreateTable(
                name: "Aseguradoras",
                columns: table => new
                {
                    IDAseguradora = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Dirección = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Teléfono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aseguradoras", x => x.IDAseguradora);
                });

            migrationBuilder.CreateTable(
                name: "Consultorios",
                columns: table => new
                {
                    IDConsultorio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Dirección = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Teléfono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultorios", x => x.IDConsultorio);
                });

            migrationBuilder.CreateTable(
                name: "MetodosDePago",
                columns: table => new
                {
                    CodigoMetodoDePago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodosDePago", x => x.CodigoMetodoDePago);
                });

            migrationBuilder.CreateTable(
                name: "PerfilUsuarios",
                columns: table => new
                {
                    CodigoDocumento = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TipoDocumento = table.Column<string>(type: "nvarchar(1)", nullable: true, defaultValue: "I"),
                    NumLicenciaMedica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Género = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Teléfono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dirección = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rol = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilUsuarios", x => x.CodigoDocumento);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    IDProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripción = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0.00m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.IDProducto);
                });

            migrationBuilder.CreateTable(
                name: "Salas",
                columns: table => new
                {
                    NumSala = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salas", x => x.NumSala);
                });

            migrationBuilder.CreateTable(
                name: "TipoServicios",
                columns: table => new
                {
                    TipoServicioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoServicios", x => x.TipoServicioId);
                });

            migrationBuilder.CreateTable(
                name: "Autorizaciones",
                columns: table => new
                {
                    IDAutorizacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaAutorizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MontoAutorizado = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0.00m),
                    IDAseguradora = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autorizaciones", x => x.IDAutorizacion);
                    table.ForeignKey(
                        name: "FK_Autorizaciones_Aseguradoras_IDAseguradora",
                        column: x => x.IDAseguradora,
                        principalTable: "Aseguradoras",
                        principalColumn: "IDAseguradora",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CuentasCobrar",
                columns: table => new
                {
                    IDCuenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0.00m),
                    Estado = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    CodigoPaciente = table.Column<string>(type: "nvarchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentasCobrar", x => x.IDCuenta);
                    table.ForeignKey(
                        name: "FK_CuentasCobrar_PerfilUsuarios_CodigoPaciente",
                        column: x => x.CodigoPaciente,
                        principalTable: "PerfilUsuarios",
                        principalColumn: "CodigoDocumento");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioCodigo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DocumentoUsuario = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UsuarioContra = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioCodigo);
                    table.ForeignKey(
                        name: "FK_Usuarios_PerfilUsuarios_DocumentoUsuario",
                        column: x => x.DocumentoUsuario,
                        principalTable: "PerfilUsuarios",
                        principalColumn: "CodigoDocumento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    ServicioCodigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripción = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TipoServicio = table.Column<int>(type: "int", nullable: true),
                    Costo = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0.00m),
                    IDAseguradora = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.ServicioCodigo);
                    table.ForeignKey(
                        name: "FK_Servicios_Aseguradoras_IDAseguradora",
                        column: x => x.IDAseguradora,
                        principalTable: "Aseguradoras",
                        principalColumn: "IDAseguradora",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servicios_TipoServicios_TipoServicio",
                        column: x => x.TipoServicio,
                        principalTable: "TipoServicios",
                        principalColumn: "TipoServicioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    ConsultaCodigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    Motivo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CodigoPaciente = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    IDConsultorio = table.Column<int>(type: "int", nullable: true),
                    IDAutorizacion = table.Column<int>(type: "int", nullable: true),
                    CodigoDocumentoMedico = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.ConsultaCodigo);
                    table.ForeignKey(
                        name: "FK_Consultas_Autorizaciones_IDAutorizacion",
                        column: x => x.IDAutorizacion,
                        principalTable: "Autorizaciones",
                        principalColumn: "IDAutorizacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultas_Consultorios_IDConsultorio",
                        column: x => x.IDConsultorio,
                        principalTable: "Consultorios",
                        principalColumn: "IDConsultorio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultas_PerfilUsuarios_CodigoPaciente",
                        column: x => x.CodigoPaciente,
                        principalTable: "PerfilUsuarios",
                        principalColumn: "CodigoDocumento");
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    IDPago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MontoPagado = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0.00m),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDCuenta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.IDPago);
                    table.ForeignKey(
                        name: "FK_Pagos_CuentasCobrar_IDCuenta",
                        column: x => x.IDCuenta,
                        principalTable: "CuentasCobrar",
                        principalColumn: "IDCuenta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservaServicios",
                columns: table => new
                {
                    ServicioCodigo = table.Column<int>(type: "int", nullable: false),
                    CodigoPaciente = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservaServicios", x => new { x.ServicioCodigo, x.CodigoPaciente });
                    table.ForeignKey(
                        name: "FK_ReservaServicios_PerfilUsuarios_CodigoPaciente",
                        column: x => x.CodigoPaciente,
                        principalTable: "PerfilUsuarios",
                        principalColumn: "CodigoDocumento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservaServicios_Servicios_ServicioCodigo",
                        column: x => x.ServicioCodigo,
                        principalTable: "Servicios",
                        principalColumn: "ServicioCodigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsultaAfecciones",
                columns: table => new
                {
                    ConsultaCodigo = table.Column<int>(type: "int", nullable: false),
                    IDAfeccion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultaAfecciones", x => new { x.ConsultaCodigo, x.IDAfeccion });
                    table.ForeignKey(
                        name: "FK_ConsultaAfecciones_Afecciones_IDAfeccion",
                        column: x => x.IDAfeccion,
                        principalTable: "Afecciones",
                        principalColumn: "IDAfeccion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsultaAfecciones_Consultas_ConsultaCodigo",
                        column: x => x.ConsultaCodigo,
                        principalTable: "Consultas",
                        principalColumn: "ConsultaCodigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsultaServicios",
                columns: table => new
                {
                    ConsultaCodigo = table.Column<int>(type: "int", nullable: false),
                    ServicioCodigo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultaServicios", x => new { x.ConsultaCodigo, x.ServicioCodigo });
                    table.ForeignKey(
                        name: "FK_ConsultaServicios_Consultas_ConsultaCodigo",
                        column: x => x.ConsultaCodigo,
                        principalTable: "Consultas",
                        principalColumn: "ConsultaCodigo");
                    table.ForeignKey(
                        name: "FK_ConsultaServicios_Servicios_ServicioCodigo",
                        column: x => x.ServicioCodigo,
                        principalTable: "Servicios",
                        principalColumn: "ServicioCodigo");
                });

            migrationBuilder.CreateTable(
                name: "Ingresos",
                columns: table => new
                {
                    IDIngreso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CostoEstancia = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0.00m),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NumSala = table.Column<int>(type: "int", nullable: true),
                    CodigoPaciente = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    CodigoDocumentoMedico = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    ConsultaCodigo = table.Column<int>(type: "int", nullable: true),
                    IDAutorizacion = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingresos", x => x.IDIngreso);
                    table.ForeignKey(
                        name: "FK_Ingresos_Autorizaciones_IDAutorizacion",
                        column: x => x.IDAutorizacion,
                        principalTable: "Autorizaciones",
                        principalColumn: "IDAutorizacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingresos_Consultas_ConsultaCodigo",
                        column: x => x.ConsultaCodigo,
                        principalTable: "Consultas",
                        principalColumn: "ConsultaCodigo");
                    table.ForeignKey(
                        name: "FK_Ingresos_PerfilUsuarios_CodigoDocumentoMedico",
                        column: x => x.CodigoDocumentoMedico,
                        principalTable: "PerfilUsuarios",
                        principalColumn: "CodigoDocumento");
                    table.ForeignKey(
                        name: "FK_Ingresos_PerfilUsuarios_CodigoPaciente",
                        column: x => x.CodigoPaciente,
                        principalTable: "PerfilUsuarios",
                        principalColumn: "CodigoDocumento");
                    table.ForeignKey(
                        name: "FK_Ingresos_Salas_NumSala",
                        column: x => x.NumSala,
                        principalTable: "Salas",
                        principalColumn: "NumSala");
                });

            migrationBuilder.CreateTable(
                name: "PrescripcionProductos",
                columns: table => new
                {
                    IDProducto = table.Column<int>(type: "int", nullable: false),
                    ConsultaCodigo = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescripcionProductos", x => new { x.IDProducto, x.ConsultaCodigo });
                    table.ForeignKey(
                        name: "FK_PrescripcionProductos_Consultas_ConsultaCodigo",
                        column: x => x.ConsultaCodigo,
                        principalTable: "Consultas",
                        principalColumn: "ConsultaCodigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescripcionProductos_Productos_IDProducto",
                        column: x => x.IDProducto,
                        principalTable: "Productos",
                        principalColumn: "IDProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    FacturaCodigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MontoTotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0.00m),
                    MontoSubtotal = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0.00m),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RNC = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodigoMetodoDePago = table.Column<int>(type: "int", nullable: false),
                    CodigoPaciente = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    IDIngreso = table.Column<int>(type: "int", nullable: true),
                    IDCuenta = table.Column<int>(type: "int", nullable: true),
                    ConsultaCodigo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.FacturaCodigo);
                    table.ForeignKey(
                        name: "FK_Facturas_Consultas_ConsultaCodigo",
                        column: x => x.ConsultaCodigo,
                        principalTable: "Consultas",
                        principalColumn: "ConsultaCodigo");
                    table.ForeignKey(
                        name: "FK_Facturas_CuentasCobrar_IDCuenta",
                        column: x => x.IDCuenta,
                        principalTable: "CuentasCobrar",
                        principalColumn: "IDCuenta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facturas_Ingresos_IDIngreso",
                        column: x => x.IDIngreso,
                        principalTable: "Ingresos",
                        principalColumn: "IDIngreso");
                    table.ForeignKey(
                        name: "FK_Facturas_MetodosDePago_CodigoMetodoDePago",
                        column: x => x.CodigoMetodoDePago,
                        principalTable: "MetodosDePago",
                        principalColumn: "CodigoMetodoDePago",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facturas_PerfilUsuarios_CodigoPaciente",
                        column: x => x.CodigoPaciente,
                        principalTable: "PerfilUsuarios",
                        principalColumn: "CodigoDocumento");
                });

            migrationBuilder.CreateTable(
                name: "IngresoAfecciones",
                columns: table => new
                {
                    IDAfeccion = table.Column<int>(type: "int", nullable: false),
                    IDIngreso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngresoAfecciones", x => new { x.IDAfeccion, x.IDIngreso });
                    table.ForeignKey(
                        name: "FK_IngresoAfecciones_Afecciones_IDAfeccion",
                        column: x => x.IDAfeccion,
                        principalTable: "Afecciones",
                        principalColumn: "IDAfeccion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngresoAfecciones_Ingresos_IDIngreso",
                        column: x => x.IDIngreso,
                        principalTable: "Ingresos",
                        principalColumn: "IDIngreso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacturaProductos",
                columns: table => new
                {
                    FacturaCodigoProducto = table.Column<int>(type: "int", nullable: false),
                    IDProducto = table.Column<int>(type: "int", nullable: false),
                    IDAutorizacion = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0.00m),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaProductos", x => new { x.FacturaCodigoProducto, x.IDProducto });
                    table.ForeignKey(
                        name: "FK_FacturaProductos_Autorizaciones_IDAutorizacion",
                        column: x => x.IDAutorizacion,
                        principalTable: "Autorizaciones",
                        principalColumn: "IDAutorizacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacturaProductos_Facturas_FacturaCodigoProducto",
                        column: x => x.FacturaCodigoProducto,
                        principalTable: "Facturas",
                        principalColumn: "FacturaCodigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacturaProductos_Productos_IDProducto",
                        column: x => x.IDProducto,
                        principalTable: "Productos",
                        principalColumn: "IDProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacturaServicios",
                columns: table => new
                {
                    FacturaCodigoServicio = table.Column<int>(type: "int", nullable: false),
                    IDProducto = table.Column<int>(type: "int", nullable: false),
                    IDAutorizacion = table.Column<int>(type: "int", nullable: true),
                    Costo = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0.00m),
                    ServicioCodigo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaServicios", x => new { x.FacturaCodigoServicio, x.IDProducto });
                    table.ForeignKey(
                        name: "FK_FacturaServicios_Autorizaciones_IDAutorizacion",
                        column: x => x.IDAutorizacion,
                        principalTable: "Autorizaciones",
                        principalColumn: "IDAutorizacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacturaServicios_Facturas_FacturaCodigoServicio",
                        column: x => x.FacturaCodigoServicio,
                        principalTable: "Facturas",
                        principalColumn: "FacturaCodigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacturaServicios_Productos_IDProducto",
                        column: x => x.IDProducto,
                        principalTable: "Productos",
                        principalColumn: "IDProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Autorizaciones_IDAseguradora",
                table: "Autorizaciones",
                column: "IDAseguradora");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultaAfecciones_IDAfeccion",
                table: "ConsultaAfecciones",
                column: "IDAfeccion");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_CodigoPaciente",
                table: "Consultas",
                column: "CodigoPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_IDAutorizacion",
                table: "Consultas",
                column: "IDAutorizacion");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_IDConsultorio",
                table: "Consultas",
                column: "IDConsultorio");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultaServicios_ServicioCodigo",
                table: "ConsultaServicios",
                column: "ServicioCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_CuentasCobrar_CodigoPaciente",
                table: "CuentasCobrar",
                column: "CodigoPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaProductos_IDAutorizacion",
                table: "FacturaProductos",
                column: "IDAutorizacion");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaProductos_IDProducto",
                table: "FacturaProductos",
                column: "IDProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_CodigoMetodoDePago",
                table: "Facturas",
                column: "CodigoMetodoDePago");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_CodigoPaciente",
                table: "Facturas",
                column: "CodigoPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ConsultaCodigo",
                table: "Facturas",
                column: "ConsultaCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_IDCuenta",
                table: "Facturas",
                column: "IDCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_IDIngreso",
                table: "Facturas",
                column: "IDIngreso");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaServicios_IDAutorizacion",
                table: "FacturaServicios",
                column: "IDAutorizacion");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaServicios_IDProducto",
                table: "FacturaServicios",
                column: "IDProducto");

            migrationBuilder.CreateIndex(
                name: "IX_IngresoAfecciones_IDIngreso",
                table: "IngresoAfecciones",
                column: "IDIngreso");

            migrationBuilder.CreateIndex(
                name: "IX_Ingresos_CodigoDocumentoMedico",
                table: "Ingresos",
                column: "CodigoDocumentoMedico");

            migrationBuilder.CreateIndex(
                name: "IX_Ingresos_CodigoPaciente",
                table: "Ingresos",
                column: "CodigoPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Ingresos_ConsultaCodigo",
                table: "Ingresos",
                column: "ConsultaCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Ingresos_IDAutorizacion",
                table: "Ingresos",
                column: "IDAutorizacion");

            migrationBuilder.CreateIndex(
                name: "IX_Ingresos_NumSala",
                table: "Ingresos",
                column: "NumSala");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_IDCuenta",
                table: "Pagos",
                column: "IDCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_PrescripcionProductos_ConsultaCodigo",
                table: "PrescripcionProductos",
                column: "ConsultaCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaServicios_CodigoPaciente",
                table: "ReservaServicios",
                column: "CodigoPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_IDAseguradora",
                table: "Servicios",
                column: "IDAseguradora");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_TipoServicio",
                table: "Servicios",
                column: "TipoServicio");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_DocumentoUsuario",
                table: "Usuarios",
                column: "DocumentoUsuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsultaAfecciones");

            migrationBuilder.DropTable(
                name: "ConsultaServicios");

            migrationBuilder.DropTable(
                name: "FacturaProductos");

            migrationBuilder.DropTable(
                name: "FacturaServicios");

            migrationBuilder.DropTable(
                name: "IngresoAfecciones");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "PrescripcionProductos");

            migrationBuilder.DropTable(
                name: "ReservaServicios");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Afecciones");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "CuentasCobrar");

            migrationBuilder.DropTable(
                name: "Ingresos");

            migrationBuilder.DropTable(
                name: "MetodosDePago");

            migrationBuilder.DropTable(
                name: "TipoServicios");

            migrationBuilder.DropTable(
                name: "Consultas");

            migrationBuilder.DropTable(
                name: "Salas");

            migrationBuilder.DropTable(
                name: "Autorizaciones");

            migrationBuilder.DropTable(
                name: "Consultorios");

            migrationBuilder.DropTable(
                name: "PerfilUsuarios");

            migrationBuilder.DropTable(
                name: "Aseguradoras");
        }
    }
}
