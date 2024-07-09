using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiHealthWave.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionDeModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AseguradoraNavigationIDAseguradora",
                table: "Servicios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoServicioId",
                table: "Servicios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCodigo",
                table: "PerfilUsuarios",
                type: "nvarchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PerfilUsuarioCodigoDocumento",
                table: "CuentasCobrar",
                type: "nvarchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AutorizacionIDAutorizacion",
                table: "Consultas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PerfilUsuarioCodigoDocumento",
                table: "Consultas",
                type: "nvarchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServicioCodigo",
                table: "Consultas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AseguradoraIDAseguradora",
                table: "Autorizaciones",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IngresoIDIngreso",
                table: "Afecciones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AfeccionConsulta",
                columns: table => new
                {
                    ConsultaCodigo = table.Column<int>(type: "int", nullable: false),
                    IdafeccionsIDAfeccion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AfeccionConsulta", x => new { x.ConsultaCodigo, x.IdafeccionsIDAfeccion });
                    table.ForeignKey(
                        name: "FK_AfeccionConsulta_Afecciones_IdafeccionsIDAfeccion",
                        column: x => x.IdafeccionsIDAfeccion,
                        principalTable: "Afecciones",
                        principalColumn: "IDAfeccion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AfeccionConsulta_Consultas_ConsultaCodigo",
                        column: x => x.ConsultaCodigo,
                        principalTable: "Consultas",
                        principalColumn: "ConsultaCodigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerfilUsuarioServicio",
                columns: table => new
                {
                    CodigoPacientesCodigoDocumento = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    ServicioCodigosServicioCodigo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilUsuarioServicio", x => new { x.CodigoPacientesCodigoDocumento, x.ServicioCodigosServicioCodigo });
                    table.ForeignKey(
                        name: "FK_PerfilUsuarioServicio_PerfilUsuarios_CodigoPacientesCodigoDocumento",
                        column: x => x.CodigoPacientesCodigoDocumento,
                        principalTable: "PerfilUsuarios",
                        principalColumn: "CodigoDocumento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PerfilUsuarioServicio_Servicios_ServicioCodigosServicioCodigo",
                        column: x => x.ServicioCodigosServicioCodigo,
                        principalTable: "Servicios",
                        principalColumn: "ServicioCodigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_AseguradoraNavigationIDAseguradora",
                table: "Servicios",
                column: "AseguradoraNavigationIDAseguradora");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_TipoServicioId",
                table: "Servicios",
                column: "TipoServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilUsuarios_UsuarioCodigo",
                table: "PerfilUsuarios",
                column: "UsuarioCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_CuentasCobrar_PerfilUsuarioCodigoDocumento",
                table: "CuentasCobrar",
                column: "PerfilUsuarioCodigoDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_AutorizacionIDAutorizacion",
                table: "Consultas",
                column: "AutorizacionIDAutorizacion");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_PerfilUsuarioCodigoDocumento",
                table: "Consultas",
                column: "PerfilUsuarioCodigoDocumento");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_ServicioCodigo",
                table: "Consultas",
                column: "ServicioCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Autorizaciones_AseguradoraIDAseguradora",
                table: "Autorizaciones",
                column: "AseguradoraIDAseguradora");

            migrationBuilder.CreateIndex(
                name: "IX_Afecciones_IngresoIDIngreso",
                table: "Afecciones",
                column: "IngresoIDIngreso");

            migrationBuilder.CreateIndex(
                name: "IX_AfeccionConsulta_IdafeccionsIDAfeccion",
                table: "AfeccionConsulta",
                column: "IdafeccionsIDAfeccion");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilUsuarioServicio_ServicioCodigosServicioCodigo",
                table: "PerfilUsuarioServicio",
                column: "ServicioCodigosServicioCodigo");

            migrationBuilder.AddForeignKey(
                name: "FK_Afecciones_Ingresos_IngresoIDIngreso",
                table: "Afecciones",
                column: "IngresoIDIngreso",
                principalTable: "Ingresos",
                principalColumn: "IDIngreso");

            migrationBuilder.AddForeignKey(
                name: "FK_Autorizaciones_Aseguradoras_AseguradoraIDAseguradora",
                table: "Autorizaciones",
                column: "AseguradoraIDAseguradora",
                principalTable: "Aseguradoras",
                principalColumn: "IDAseguradora");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Autorizaciones_AutorizacionIDAutorizacion",
                table: "Consultas",
                column: "AutorizacionIDAutorizacion",
                principalTable: "Autorizaciones",
                principalColumn: "IDAutorizacion");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_PerfilUsuarios_PerfilUsuarioCodigoDocumento",
                table: "Consultas",
                column: "PerfilUsuarioCodigoDocumento",
                principalTable: "PerfilUsuarios",
                principalColumn: "CodigoDocumento");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultas_Servicios_ServicioCodigo",
                table: "Consultas",
                column: "ServicioCodigo",
                principalTable: "Servicios",
                principalColumn: "ServicioCodigo");

            migrationBuilder.AddForeignKey(
                name: "FK_CuentasCobrar_PerfilUsuarios_PerfilUsuarioCodigoDocumento",
                table: "CuentasCobrar",
                column: "PerfilUsuarioCodigoDocumento",
                principalTable: "PerfilUsuarios",
                principalColumn: "CodigoDocumento");

            migrationBuilder.AddForeignKey(
                name: "FK_PerfilUsuarios_Usuarios_UsuarioCodigo",
                table: "PerfilUsuarios",
                column: "UsuarioCodigo",
                principalTable: "Usuarios",
                principalColumn: "UsuarioCodigo");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_Aseguradoras_AseguradoraNavigationIDAseguradora",
                table: "Servicios",
                column: "AseguradoraNavigationIDAseguradora",
                principalTable: "Aseguradoras",
                principalColumn: "IDAseguradora");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicios_TipoServicios_TipoServicioId",
                table: "Servicios",
                column: "TipoServicioId",
                principalTable: "TipoServicios",
                principalColumn: "TipoServicioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Afecciones_Ingresos_IngresoIDIngreso",
                table: "Afecciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Autorizaciones_Aseguradoras_AseguradoraIDAseguradora",
                table: "Autorizaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Autorizaciones_AutorizacionIDAutorizacion",
                table: "Consultas");

            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_PerfilUsuarios_PerfilUsuarioCodigoDocumento",
                table: "Consultas");

            migrationBuilder.DropForeignKey(
                name: "FK_Consultas_Servicios_ServicioCodigo",
                table: "Consultas");

            migrationBuilder.DropForeignKey(
                name: "FK_CuentasCobrar_PerfilUsuarios_PerfilUsuarioCodigoDocumento",
                table: "CuentasCobrar");

            migrationBuilder.DropForeignKey(
                name: "FK_PerfilUsuarios_Usuarios_UsuarioCodigo",
                table: "PerfilUsuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_Aseguradoras_AseguradoraNavigationIDAseguradora",
                table: "Servicios");

            migrationBuilder.DropForeignKey(
                name: "FK_Servicios_TipoServicios_TipoServicioId",
                table: "Servicios");

            migrationBuilder.DropTable(
                name: "AfeccionConsulta");

            migrationBuilder.DropTable(
                name: "PerfilUsuarioServicio");

            migrationBuilder.DropIndex(
                name: "IX_Servicios_AseguradoraNavigationIDAseguradora",
                table: "Servicios");

            migrationBuilder.DropIndex(
                name: "IX_Servicios_TipoServicioId",
                table: "Servicios");

            migrationBuilder.DropIndex(
                name: "IX_PerfilUsuarios_UsuarioCodigo",
                table: "PerfilUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_CuentasCobrar_PerfilUsuarioCodigoDocumento",
                table: "CuentasCobrar");

            migrationBuilder.DropIndex(
                name: "IX_Consultas_AutorizacionIDAutorizacion",
                table: "Consultas");

            migrationBuilder.DropIndex(
                name: "IX_Consultas_PerfilUsuarioCodigoDocumento",
                table: "Consultas");

            migrationBuilder.DropIndex(
                name: "IX_Consultas_ServicioCodigo",
                table: "Consultas");

            migrationBuilder.DropIndex(
                name: "IX_Autorizaciones_AseguradoraIDAseguradora",
                table: "Autorizaciones");

            migrationBuilder.DropIndex(
                name: "IX_Afecciones_IngresoIDIngreso",
                table: "Afecciones");

            migrationBuilder.DropColumn(
                name: "AseguradoraNavigationIDAseguradora",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "TipoServicioId",
                table: "Servicios");

            migrationBuilder.DropColumn(
                name: "UsuarioCodigo",
                table: "PerfilUsuarios");

            migrationBuilder.DropColumn(
                name: "PerfilUsuarioCodigoDocumento",
                table: "CuentasCobrar");

            migrationBuilder.DropColumn(
                name: "AutorizacionIDAutorizacion",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "PerfilUsuarioCodigoDocumento",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "ServicioCodigo",
                table: "Consultas");

            migrationBuilder.DropColumn(
                name: "AseguradoraIDAseguradora",
                table: "Autorizaciones");

            migrationBuilder.DropColumn(
                name: "IngresoIDIngreso",
                table: "Afecciones");
        }
    }
}
