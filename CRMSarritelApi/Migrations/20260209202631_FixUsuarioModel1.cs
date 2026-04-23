using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CRMSarritelApi.Migrations
{
    /// <inheritdoc />
    public partial class FixUsuarioModel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    FechaCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Dni = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Tipo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Movil = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    NumeroCuenta = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Penalizado = table.Column<bool>(type: "boolean", nullable: false),
                    NotaPublica = table.Column<string>(type: "text", nullable: true),
                    NotaPrivada = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Direccion_Calle = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Direccion_CodigoPostal = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Direccion_Poblacion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Direccion_Provincia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comerciales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comerciales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provincias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sucursales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false, comment: "BCrypt hash (60 chars)"),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Precio = table.Column<decimal>(type: "numeric", nullable: false),
                    PrecioOferta = table.Column<decimal>(type: "numeric", nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    Imagen = table.Column<string>(type: "text", nullable: false),
                    FechaCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CategoriaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Municipios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    CodigoPostal = table.Column<string>(type: "text", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false),
                    IdProvincia = table.Column<int>(type: "integer", nullable: false),
                    ProvinciaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Municipios_Provincias_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "Provincias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolUsuario",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "integer", nullable: false),
                    UsuariosId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolUsuario", x => new { x.RolesId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_RolUsuario_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolUsuario_Usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRoles",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    RolId = table.Column<int>(type: "integer", nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRoles", x => new { x.UsuarioId, x.RolId });
                    table.ForeignKey(
                        name: "FK_UsuarioRoles_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRoles_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contrato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdCliente = table.Column<int>(type: "integer", nullable: true),
                    IdComercial = table.Column<int>(type: "integer", nullable: true),
                    IdProducto = table.Column<int>(type: "integer", nullable: true),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Estado = table.Column<string>(type: "text", nullable: true),
                    UrlContrato = table.Column<string>(type: "text", nullable: true),
                    CheckContrato = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ArchivoBytes = table.Column<byte[]>(type: "bytea", nullable: true),
                    NombreArchivo = table.Column<string>(type: "text", nullable: true),
                    TipoMime = table.Column<string>(type: "text", nullable: true),
                    TamanoArchivo = table.Column<long>(type: "bigint", nullable: true),
                    VersionDocumento = table.Column<int>(type: "integer", nullable: false),
                    FechaModificacionArchivo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IdUsuarioSubida = table.Column<int>(type: "integer", nullable: true),
                    ComentariosArchivo = table.Column<string>(type: "text", nullable: true),
                    ClienteId = table.Column<int>(type: "integer", nullable: true),
                    ComercialId = table.Column<int>(type: "integer", nullable: true),
                    ProductoId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contrato_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contrato_Comerciales_ComercialId",
                        column: x => x.ComercialId,
                        principalTable: "Comerciales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contrato_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VentaId = table.Column<int>(type: "integer", nullable: false),
                    NumeroVenta = table.Column<string>(type: "text", nullable: false),
                    TipoVenta_Nombre = table.Column<string>(type: "text", nullable: false),
                    TipoVenta_Codigo = table.Column<string>(type: "text", nullable: true),
                    TipoVenta_Descripcion = table.Column<string>(type: "text", nullable: true),
                    TipoVenta_Activo = table.Column<bool>(type: "boolean", nullable: false),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    ProductoPrincipalId = table.Column<int>(type: "integer", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaVenta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaInstalacionPrevista = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaInstalacionReal = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Estado_Codigo = table.Column<string>(type: "text", nullable: false),
                    Estado_Nombre = table.Column<string>(type: "text", nullable: false),
                    Estado_Icono = table.Column<string>(type: "text", nullable: false),
                    Estado_Color = table.Column<string>(type: "text", nullable: false),
                    Estado_Orden = table.Column<short>(type: "smallint", nullable: false),
                    Estado_PermiteEdicion = table.Column<bool>(type: "boolean", nullable: false),
                    Estado_PermiteEliminar = table.Column<bool>(type: "boolean", nullable: false),
                    Estado_EsFinal = table.Column<bool>(type: "boolean", nullable: false),
                    Estado_Activo = table.Column<bool>(type: "boolean", nullable: false),
                    Estado_EsInicial = table.Column<bool>(type: "boolean", nullable: false),
                    Estado_UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    EtapaActual = table.Column<short>(type: "smallint", nullable: false),
                    MontoVenta = table.Column<decimal>(type: "numeric", nullable: false),
                    DescuentoPorcentaje = table.Column<decimal>(type: "numeric", nullable: false),
                    DescuentoMonto = table.Column<decimal>(type: "numeric", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    Notas = table.Column<string>(type: "text", nullable: true),
                    ArchivoContrato = table.Column<string>(type: "text", nullable: true),
                    CreadoPorId = table.Column<int>(type: "integer", nullable: true),
                    OrigenVenta = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ventas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ventas_Comerciales_CreadoPorId",
                        column: x => x.CreadoPorId,
                        principalTable: "Comerciales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Ventas_Comerciales_Estado_UsuarioId",
                        column: x => x.Estado_UsuarioId,
                        principalTable: "Comerciales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ventas_Comerciales_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Comerciales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ventas_Productos_ProductoPrincipalId",
                        column: x => x.ProductoPrincipalId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CodigosPostales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Localidad = table.Column<string>(type: "text", nullable: false),
                    ProvinciaId = table.Column<int>(type: "integer", nullable: false),
                    MunicipioId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodigosPostales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodigosPostales_Municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CodigosPostales_Provincias_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "Provincias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comisiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VentaId = table.Column<int>(type: "integer", nullable: true),
                    EmpleadoId = table.Column<int>(type: "integer", nullable: false),
                    Periodo = table.Column<string>(type: "text", nullable: true),
                    Tipo_Codigo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Tipo_Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TasaPorcentaje = table.Column<decimal>(type: "numeric", nullable: true),
                    MontoFijo = table.Column<decimal>(type: "numeric", nullable: true),
                    BaseCalculo = table.Column<decimal>(type: "numeric", nullable: false),
                    MontoComision = table.Column<decimal>(type: "numeric", nullable: false),
                    Estado_Codigo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Estado_Nombre = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Estado_Color = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Estado_Icono = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Estado_EsPagable = table.Column<bool>(type: "boolean", nullable: false),
                    Estado_EsFinal = table.Column<bool>(type: "boolean", nullable: false),
                    Estado_Orden = table.Column<short>(type: "smallint", nullable: false),
                    Notas = table.Column<string>(type: "text", nullable: true),
                    FechaCalculo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ComercialId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comisiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comisiones_Comerciales_ComercialId",
                        column: x => x.ComercialId,
                        principalTable: "Comerciales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comisiones_Comerciales_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Comerciales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comisiones_Ventas_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleVentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    IdVenta = table.Column<int>(type: "integer", nullable: true),
                    IdProducto = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleVentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Ventas_IdVenta",
                        column: x => x.IdVenta,
                        principalTable: "Ventas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Municipios",
                columns: new[] { "Id", "Activo", "CodigoPostal", "IdProvincia", "Nombre", "ProvinciaId" },
                values: new object[,]
                {
                    { 1001, true, null, 1, "Alegría-Dulantzi", null },
                    { 1002, true, null, 1, "Amurrio", null },
                    { 1003, true, null, 1, "Añana", null },
                    { 1059, true, null, 1, "Artziniega", null },
                    { 1901, true, null, 1, "Vitoria-Gasteiz", null },
                    { 2001, true, null, 2, "Abengibre", null },
                    { 2002, true, null, 2, "Alatoz", null },
                    { 2003, true, null, 2, "Albacete", null },
                    { 2004, true, null, 2, "Albatana", null },
                    { 2901, true, null, 2, "Albacete", null },
                    { 3001, true, null, 3, "Adsubia", null },
                    { 3002, true, null, 3, "Agost", null },
                    { 3003, true, null, 3, "Agres", null },
                    { 3013, true, null, 3, "Alicante/Alacant", null },
                    { 3901, true, null, 3, "Alicante/Alacant", null },
                    { 4001, true, null, 4, "Abla", null },
                    { 4002, true, null, 4, "Abrucena", null },
                    { 4003, true, null, 4, "Adra", null },
                    { 4004, true, null, 4, "Albánchez", null },
                    { 4901, true, null, 4, "Almería", null },
                    { 8001, true, null, 8, "Abrera", null },
                    { 8002, true, null, 8, "Aguilar de Segarra", null },
                    { 8003, true, null, 8, "Alella", null },
                    { 8004, true, null, 8, "Alpens", null },
                    { 8019, true, null, 8, "Barcelona", null },
                    { 8901, true, null, 8, "Barcelona", null },
                    { 28001, true, null, 28, "Acebeda (La)", null },
                    { 28002, true, null, 28, "Ajalvir", null },
                    { 28003, true, null, 28, "Alameda del Valle", null },
                    { 28004, true, null, 28, "Alcalá de Henares", null },
                    { 28005, true, null, 28, "Alcobendas", null },
                    { 28006, true, null, 28, "Alcorcón", null },
                    { 28079, true, null, 28, "Madrid", null },
                    { 28901, true, null, 28, "Madrid", null },
                    { 35001, true, null, 35, "Agaete", null },
                    { 35002, true, null, 35, "Agüimes", null },
                    { 35003, true, null, 35, "Antigua", null },
                    { 35013, true, null, 35, "Las Palmas de Gran Canaria", null },
                    { 35901, true, null, 35, "Las Palmas de Gran Canaria", null },
                    { 38001, true, null, 38, "Adeje", null },
                    { 38002, true, null, 38, "Agulo", null },
                    { 38003, true, null, 38, "Alajeró", null },
                    { 38022, true, null, 38, "Santa Cruz de Tenerife", null },
                    { 38901, true, null, 38, "Santa Cruz de Tenerife", null },
                    { 51001, true, null, 51, "Ceuta", null },
                    { 51901, true, null, 51, "Ceuta", null },
                    { 52001, true, null, 52, "Melilla", null },
                    { 52901, true, null, 52, "Melilla", null }
                });

            migrationBuilder.InsertData(
                table: "Provincias",
                columns: new[] { "Id", "Codigo", "Nombre" },
                values: new object[,]
                {
                    { 1, "01", "Araba/Álava" },
                    { 2, "02", "Albacete" },
                    { 3, "03", "Alacant/Alicante" },
                    { 4, "04", "Almería" },
                    { 5, "05", "Ávila" },
                    { 6, "06", "Badajoz" },
                    { 7, "07", "Illes Balears" },
                    { 8, "08", "Barcelona" },
                    { 9, "09", "Burgos" },
                    { 10, "10", "Cáceres" },
                    { 11, "11", "Cádiz" },
                    { 12, "12", "Castelló/Castellón" },
                    { 13, "13", "Ciudad Real" },
                    { 14, "14", "Córdoba" },
                    { 15, "15", "A Coruña" },
                    { 16, "16", "Cuenca" },
                    { 17, "17", "Girona" },
                    { 18, "18", "Granada" },
                    { 19, "19", "Guadalajara" },
                    { 20, "20", "Gipuzkoa" },
                    { 21, "21", "Huelva" },
                    { 22, "22", "Huesca" },
                    { 23, "23", "Jaén" },
                    { 24, "24", "León" },
                    { 25, "25", "Lleida" },
                    { 26, "26", "La Rioja" },
                    { 27, "27", "Lugo" },
                    { 28, "28", "Madrid" },
                    { 29, "29", "Málaga" },
                    { 30, "30", "Murcia" },
                    { 31, "31", "Navarra" },
                    { 32, "32", "Ourense" },
                    { 33, "33", "Asturias" },
                    { 34, "34", "Palencia" },
                    { 35, "35", "Las Palmas" },
                    { 36, "36", "Pontevedra" },
                    { 37, "37", "Salamanca" },
                    { 38, "38", "Santa Cruz de Tenerife" },
                    { 39, "39", "Cantabria" },
                    { 40, "40", "Segovia" },
                    { 41, "41", "Sevilla" },
                    { 42, "42", "Soria" },
                    { 43, "43", "Tarragona" },
                    { 44, "44", "Teruel" },
                    { 45, "45", "Toledo" },
                    { 46, "46", "Valencia/València" },
                    { 47, "47", "Valladolid" },
                    { 48, "48", "Bizkaia" },
                    { 49, "49", "Zamora" },
                    { 50, "50", "Zaragoza" },
                    { 51, "51", "Ceuta" },
                    { 52, "52", "Melilla" }
                });

            migrationBuilder.InsertData(
                table: "CodigosPostales",
                columns: new[] { "Id", "Codigo", "Localidad", "MunicipioId", "ProvinciaId", "Tipo" },
                values: new object[,]
                {
                    { 1, "01240", "Alegría-Dulantzi", 1001, 1, "Municipio" },
                    { 2, "01470", "Amurrio", 1002, 1, "Municipio" },
                    { 3, "01426", "Añana", 1003, 1, "Municipio" },
                    { 4, "01474", "Artziniega", 1059, 1, "Municipio" },
                    { 5, "01001", "Vitoria-Gasteiz", 1901, 1, "Capital" },
                    { 6, "01002", "Vitoria-Gasteiz", 1901, 1, "Capital" },
                    { 7, "01003", "Vitoria-Gasteiz", 1901, 1, "Capital" },
                    { 8, "01004", "Vitoria-Gasteiz", 1901, 1, "Capital" },
                    { 9, "01005", "Vitoria-Gasteiz", 1901, 1, "Capital" },
                    { 10, "01006", "Vitoria-Gasteiz", 1901, 1, "Capital" },
                    { 11, "01007", "Vitoria-Gasteiz", 1901, 1, "Capital" },
                    { 12, "01008", "Vitoria-Gasteiz", 1901, 1, "Capital" },
                    { 13, "01009", "Vitoria-Gasteiz", 1901, 1, "Capital" },
                    { 14, "01010", "Vitoria-Gasteiz", 1901, 1, "Capital" },
                    { 15, "01011", "Vitoria-Gasteiz", 1901, 1, "Capital" },
                    { 16, "01012", "Vitoria-Gasteiz", 1901, 1, "Capital" },
                    { 17, "01013", "Vitoria-Gasteiz", 1901, 1, "Capital" },
                    { 18, "01014", "Vitoria-Gasteiz", 1901, 1, "Capital" },
                    { 19, "01015", "Vitoria-Gasteiz", 1901, 1, "Capital" },
                    { 20, "02250", "Abengibre", 2001, 2, "Municipio" },
                    { 21, "02152", "Alatoz", 2002, 2, "Municipio" },
                    { 22, "02001", "Albacete", 2901, 2, "Capital" },
                    { 23, "02002", "Albacete", 2901, 2, "Capital" },
                    { 24, "02003", "Albacete", 2901, 2, "Capital" },
                    { 25, "02004", "Albacete", 2901, 2, "Capital" },
                    { 26, "02005", "Albacete", 2901, 2, "Capital" },
                    { 27, "02006", "Albacete", 2901, 2, "Capital" },
                    { 28, "02007", "Albacete", 2901, 2, "Capital" },
                    { 29, "02008", "Albacete", 2901, 2, "Capital" },
                    { 30, "02653", "Albatana", 2004, 2, "Municipio" },
                    { 31, "03786", "Adsubia", 3001, 3, "Municipio" },
                    { 32, "03698", "Agost", 3002, 3, "Municipio" },
                    { 33, "03870", "Agres", 3003, 3, "Municipio" },
                    { 34, "03001", "Alicante/Alacant", 3901, 3, "Capital" },
                    { 35, "03002", "Alicante/Alacant", 3901, 3, "Capital" },
                    { 36, "03003", "Alicante/Alacant", 3901, 3, "Capital" },
                    { 37, "03004", "Alicante/Alacant", 3901, 3, "Capital" },
                    { 38, "03005", "Alicante/Alacant", 3901, 3, "Capital" },
                    { 39, "03006", "Alicante/Alacant", 3901, 3, "Capital" },
                    { 40, "03007", "Alicante/Alacant", 3901, 3, "Capital" },
                    { 41, "03008", "Alicante/Alacant", 3901, 3, "Capital" },
                    { 42, "03009", "Alicante/Alacant", 3901, 3, "Capital" },
                    { 43, "03010", "Alicante/Alacant", 3901, 3, "Capital" },
                    { 44, "03011", "Alicante/Alacant", 3901, 3, "Capital" },
                    { 45, "03012", "Alicante/Alacant", 3901, 3, "Capital" },
                    { 46, "03013", "Alicante/Alacant", 3901, 3, "Capital" },
                    { 47, "03014", "Alicante/Alacant", 3901, 3, "Capital" },
                    { 48, "03015", "Alicante/Alacant", 3901, 3, "Capital" },
                    { 49, "03016", "Alicante/Alacant", 3901, 3, "Capital" },
                    { 50, "04510", "Abla", 4001, 4, "Municipio" },
                    { 51, "04520", "Abrucena", 4002, 4, "Municipio" },
                    { 52, "04770", "Adra", 4003, 4, "Municipio" },
                    { 53, "04857", "Albánchez", 4004, 4, "Municipio" },
                    { 54, "04001", "Almería", 4901, 4, "Capital" },
                    { 55, "04002", "Almería", 4901, 4, "Capital" },
                    { 56, "04003", "Almería", 4901, 4, "Capital" },
                    { 57, "04004", "Almería", 4901, 4, "Capital" },
                    { 58, "04005", "Almería", 4901, 4, "Capital" },
                    { 59, "04006", "Almería", 4901, 4, "Capital" },
                    { 60, "04007", "Almería", 4901, 4, "Capital" },
                    { 61, "04008", "Almería", 4901, 4, "Capital" },
                    { 62, "04009", "Almería", 4901, 4, "Capital" },
                    { 63, "08630", "Abrera", 8001, 8, "Municipio" },
                    { 64, "08256", "Aguilar de Segarra", 8002, 8, "Municipio" },
                    { 65, "08328", "Alella", 8003, 8, "Municipio" },
                    { 66, "08587", "Alpens", 8004, 8, "Municipio" },
                    { 67, "08001", "Barcelona", 8901, 8, "Capital" },
                    { 68, "08002", "Barcelona", 8901, 8, "Capital" },
                    { 69, "08003", "Barcelona", 8901, 8, "Capital" },
                    { 70, "08004", "Barcelona", 8901, 8, "Capital" },
                    { 71, "08005", "Barcelona", 8901, 8, "Capital" },
                    { 72, "08006", "Barcelona", 8901, 8, "Capital" },
                    { 73, "08007", "Barcelona", 8901, 8, "Capital" },
                    { 74, "08008", "Barcelona", 8901, 8, "Capital" },
                    { 75, "08009", "Barcelona", 8901, 8, "Capital" },
                    { 76, "08010", "Barcelona", 8901, 8, "Capital" },
                    { 77, "08011", "Barcelona", 8901, 8, "Capital" },
                    { 78, "08012", "Barcelona", 8901, 8, "Capital" },
                    { 79, "08013", "Barcelona", 8901, 8, "Capital" },
                    { 80, "08014", "Barcelona", 8901, 8, "Capital" },
                    { 81, "08015", "Barcelona", 8901, 8, "Capital" },
                    { 82, "08016", "Barcelona", 8901, 8, "Capital" },
                    { 83, "08017", "Barcelona", 8901, 8, "Capital" },
                    { 84, "08018", "Barcelona", 8901, 8, "Capital" },
                    { 85, "08019", "Barcelona", 8901, 8, "Capital" },
                    { 86, "08020", "Barcelona", 8901, 8, "Capital" },
                    { 87, "08021", "Barcelona", 8901, 8, "Capital" },
                    { 88, "08022", "Barcelona", 8901, 8, "Capital" },
                    { 89, "08023", "Barcelona", 8901, 8, "Capital" },
                    { 90, "08024", "Barcelona", 8901, 8, "Capital" },
                    { 91, "08025", "Barcelona", 8901, 8, "Capital" },
                    { 92, "08026", "Barcelona", 8901, 8, "Capital" },
                    { 93, "08027", "Barcelona", 8901, 8, "Capital" },
                    { 94, "08028", "Barcelona", 8901, 8, "Capital" },
                    { 95, "08029", "Barcelona", 8901, 8, "Capital" },
                    { 96, "08030", "Barcelona", 8901, 8, "Capital" },
                    { 97, "08031", "Barcelona", 8901, 8, "Capital" },
                    { 98, "08032", "Barcelona", 8901, 8, "Capital" },
                    { 99, "08033", "Barcelona", 8901, 8, "Capital" },
                    { 100, "08034", "Barcelona", 8901, 8, "Capital" },
                    { 101, "08035", "Barcelona", 8901, 8, "Capital" },
                    { 102, "08036", "Barcelona", 8901, 8, "Capital" },
                    { 103, "08037", "Barcelona", 8901, 8, "Capital" },
                    { 104, "08038", "Barcelona", 8901, 8, "Capital" },
                    { 105, "08039", "Barcelona", 8901, 8, "Capital" },
                    { 106, "08040", "Barcelona", 8901, 8, "Capital" },
                    { 107, "08041", "Barcelona", 8901, 8, "Capital" },
                    { 108, "08042", "Barcelona", 8901, 8, "Capital" },
                    { 109, "28755", "Acebeda (La)", 28001, 28, "Municipio" },
                    { 110, "28864", "Ajalvir", 28002, 28, "Municipio" },
                    { 111, "28749", "Alameda del Valle", 28003, 28, "Municipio" },
                    { 112, "28801", "Alcalá de Henares", 28004, 28, "Municipio" },
                    { 113, "28802", "Alcalá de Henares", 28004, 28, "Municipio" },
                    { 114, "28803", "Alcalá de Henares", 28004, 28, "Municipio" },
                    { 115, "28804", "Alcalá de Henares", 28004, 28, "Municipio" },
                    { 116, "28805", "Alcalá de Henares", 28004, 28, "Municipio" },
                    { 117, "28806", "Alcalá de Henares", 28004, 28, "Municipio" },
                    { 118, "28807", "Alcalá de Henares", 28004, 28, "Municipio" },
                    { 119, "28100", "Alcobendas", 28005, 28, "Municipio" },
                    { 120, "28101", "Alcobendas", 28005, 28, "Municipio" },
                    { 121, "28102", "Alcobendas", 28005, 28, "Municipio" },
                    { 122, "28103", "Alcobendas", 28005, 28, "Municipio" },
                    { 123, "28001", "Madrid", 28901, 28, "Capital" },
                    { 124, "28002", "Madrid", 28901, 28, "Capital" },
                    { 125, "28003", "Madrid", 28901, 28, "Capital" },
                    { 126, "28004", "Madrid", 28901, 28, "Capital" },
                    { 127, "28005", "Madrid", 28901, 28, "Capital" },
                    { 128, "28006", "Madrid", 28901, 28, "Capital" },
                    { 129, "28007", "Madrid", 28901, 28, "Capital" },
                    { 130, "28008", "Madrid", 28901, 28, "Capital" },
                    { 131, "28009", "Madrid", 28901, 28, "Capital" },
                    { 132, "28010", "Madrid", 28901, 28, "Capital" },
                    { 133, "28011", "Madrid", 28901, 28, "Capital" },
                    { 134, "28012", "Madrid", 28901, 28, "Capital" },
                    { 135, "28013", "Madrid", 28901, 28, "Capital" },
                    { 136, "28014", "Madrid", 28901, 28, "Capital" },
                    { 137, "28015", "Madrid", 28901, 28, "Capital" },
                    { 138, "28016", "Madrid", 28901, 28, "Capital" },
                    { 139, "28017", "Madrid", 28901, 28, "Capital" },
                    { 140, "28018", "Madrid", 28901, 28, "Capital" },
                    { 141, "28019", "Madrid", 28901, 28, "Capital" },
                    { 142, "28020", "Madrid", 28901, 28, "Capital" },
                    { 143, "28021", "Madrid", 28901, 28, "Capital" },
                    { 144, "28022", "Madrid", 28901, 28, "Capital" },
                    { 145, "28023", "Madrid", 28901, 28, "Capital" },
                    { 146, "28024", "Madrid", 28901, 28, "Capital" },
                    { 147, "28025", "Madrid", 28901, 28, "Capital" },
                    { 148, "28026", "Madrid", 28901, 28, "Capital" },
                    { 149, "28027", "Madrid", 28901, 28, "Capital" },
                    { 150, "28028", "Madrid", 28901, 28, "Capital" },
                    { 151, "28029", "Madrid", 28901, 28, "Capital" },
                    { 152, "28030", "Madrid", 28901, 28, "Capital" },
                    { 153, "28031", "Madrid", 28901, 28, "Capital" },
                    { 154, "28032", "Madrid", 28901, 28, "Capital" },
                    { 155, "28033", "Madrid", 28901, 28, "Capital" },
                    { 156, "28034", "Madrid", 28901, 28, "Capital" },
                    { 157, "28035", "Madrid", 28901, 28, "Capital" },
                    { 158, "28036", "Madrid", 28901, 28, "Capital" },
                    { 159, "28037", "Madrid", 28901, 28, "Capital" },
                    { 160, "28038", "Madrid", 28901, 28, "Capital" },
                    { 161, "28039", "Madrid", 28901, 28, "Capital" },
                    { 162, "28040", "Madrid", 28901, 28, "Capital" },
                    { 163, "28041", "Madrid", 28901, 28, "Capital" },
                    { 164, "28042", "Madrid", 28901, 28, "Capital" },
                    { 165, "28043", "Madrid", 28901, 28, "Capital" },
                    { 166, "28044", "Madrid", 28901, 28, "Capital" },
                    { 167, "28045", "Madrid", 28901, 28, "Capital" },
                    { 168, "28046", "Madrid", 28901, 28, "Capital" },
                    { 169, "28047", "Madrid", 28901, 28, "Capital" },
                    { 170, "28048", "Madrid", 28901, 28, "Capital" },
                    { 171, "28049", "Madrid", 28901, 28, "Capital" },
                    { 172, "28050", "Madrid", 28901, 28, "Capital" },
                    { 173, "28051", "Madrid", 28901, 28, "Capital" },
                    { 174, "28052", "Madrid", 28901, 28, "Capital" },
                    { 175, "28053", "Madrid", 28901, 28, "Capital" },
                    { 176, "28054", "Madrid", 28901, 28, "Capital" },
                    { 177, "28055", "Madrid", 28901, 28, "Capital" },
                    { 178, "28056", "Madrid", 28901, 28, "Capital" },
                    { 179, "28057", "Madrid", 28901, 28, "Capital" },
                    { 180, "28058", "Madrid", 28901, 28, "Capital" },
                    { 181, "28059", "Madrid", 28901, 28, "Capital" },
                    { 182, "28060", "Madrid", 28901, 28, "Capital" },
                    { 183, "28061", "Madrid", 28901, 28, "Capital" },
                    { 184, "28062", "Madrid", 28901, 28, "Capital" },
                    { 185, "28063", "Madrid", 28901, 28, "Capital" },
                    { 186, "28064", "Madrid", 28901, 28, "Capital" },
                    { 187, "28065", "Madrid", 28901, 28, "Capital" },
                    { 188, "28066", "Madrid", 28901, 28, "Capital" },
                    { 189, "28067", "Madrid", 28901, 28, "Capital" },
                    { 190, "28068", "Madrid", 28901, 28, "Capital" },
                    { 191, "28069", "Madrid", 28901, 28, "Capital" },
                    { 192, "28070", "Madrid", 28901, 28, "Capital" },
                    { 193, "28071", "Madrid", 28901, 28, "Capital" },
                    { 194, "28072", "Madrid", 28901, 28, "Capital" },
                    { 195, "28073", "Madrid", 28901, 28, "Capital" },
                    { 196, "28074", "Madrid", 28901, 28, "Capital" },
                    { 197, "28075", "Madrid", 28901, 28, "Capital" },
                    { 198, "28076", "Madrid", 28901, 28, "Capital" },
                    { 199, "28077", "Madrid", 28901, 28, "Capital" },
                    { 200, "28078", "Madrid", 28901, 28, "Capital" },
                    { 201, "28079", "Madrid", 28901, 28, "Capital" },
                    { 202, "28080", "Madrid", 28901, 28, "Capital" },
                    { 203, "35480", "Agaete", 35001, 35, "Municipio" },
                    { 204, "35260", "Agüimes", 35002, 35, "Municipio" },
                    { 205, "35261", "Agüimes", 35002, 35, "Municipio" },
                    { 206, "35262", "Agüimes", 35002, 35, "Municipio" },
                    { 207, "35263", "Agüimes", 35002, 35, "Municipio" },
                    { 208, "35264", "Agüimes", 35002, 35, "Municipio" },
                    { 209, "35265", "Agüimes", 35002, 35, "Municipio" },
                    { 210, "35266", "Agüimes", 35002, 35, "Municipio" },
                    { 211, "35267", "Agüimes", 35002, 35, "Municipio" },
                    { 212, "35268", "Agüimes", 35002, 35, "Municipio" },
                    { 213, "35269", "Agüimes", 35002, 35, "Municipio" },
                    { 214, "35630", "Antigua", 35003, 35, "Municipio" },
                    { 215, "35001", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 216, "35002", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 217, "35003", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 218, "35004", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 219, "35005", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 220, "35006", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 221, "35007", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 222, "35008", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 223, "35009", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 224, "35010", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 225, "35011", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 226, "35012", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 227, "35013", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 228, "35014", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 229, "35015", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 230, "35016", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 231, "35017", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 232, "35018", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 233, "35019", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 234, "35020", "Las Palmas de Gran Canaria", 35901, 35, "Capital" },
                    { 235, "38670", "Adeje", 38001, 38, "Municipio" },
                    { 236, "38820", "Agulo", 38002, 38, "Municipio" },
                    { 237, "38812", "Alajeró", 38003, 38, "Municipio" },
                    { 238, "38001", "Santa Cruz de Tenerife", 38901, 38, "Capital" },
                    { 239, "38002", "Santa Cruz de Tenerife", 38901, 38, "Capital" },
                    { 240, "38003", "Santa Cruz de Tenerife", 38901, 38, "Capital" },
                    { 241, "38004", "Santa Cruz de Tenerife", 38901, 38, "Capital" },
                    { 242, "38005", "Santa Cruz de Tenerife", 38901, 38, "Capital" },
                    { 243, "38006", "Santa Cruz de Tenerife", 38901, 38, "Capital" },
                    { 244, "38007", "Santa Cruz de Tenerife", 38901, 38, "Capital" },
                    { 245, "38008", "Santa Cruz de Tenerife", 38901, 38, "Capital" },
                    { 246, "38009", "Santa Cruz de Tenerife", 38901, 38, "Capital" },
                    { 247, "38010", "Santa Cruz de Tenerife", 38901, 38, "Capital" },
                    { 248, "51001", "Ceuta", 51901, 51, "Capital" },
                    { 249, "51002", "Ceuta", 51901, 51, "Capital" },
                    { 250, "51003", "Ceuta", 51901, 51, "Capital" },
                    { 251, "51004", "Ceuta", 51901, 51, "Capital" },
                    { 252, "51005", "Ceuta", 51901, 51, "Capital" },
                    { 253, "52001", "Melilla", 52901, 52, "Capital" },
                    { 254, "52002", "Melilla", 52901, 52, "Capital" },
                    { 255, "52003", "Melilla", 52901, 52, "Capital" },
                    { 256, "52004", "Melilla", 52901, 52, "Capital" },
                    { 257, "52005", "Melilla", 52901, 52, "Capital" },
                    { 258, "52006", "Melilla", 52901, 52, "Capital" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodigosPostales_MunicipioId",
                table: "CodigosPostales",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_CodigosPostales_ProvinciaId",
                table: "CodigosPostales",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Comisiones_ComercialId",
                table: "Comisiones",
                column: "ComercialId");

            migrationBuilder.CreateIndex(
                name: "IX_Comisiones_EmpleadoId",
                table: "Comisiones",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comisiones_VentaId",
                table: "Comisiones",
                column: "VentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_ClienteId",
                table: "Contrato",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_ComercialId",
                table: "Contrato",
                column: "ComercialId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_ProductoId",
                table: "Contrato",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_IdProducto",
                table: "DetalleVentas",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_IdVenta",
                table: "DetalleVentas",
                column: "IdVenta");

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_ProvinciaId",
                table: "Municipios",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_RolUsuario_UsuariosId",
                table: "RolUsuario",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRoles_RolId",
                table: "UsuarioRoles",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ClienteId",
                table: "Ventas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_CreadoPorId",
                table: "Ventas",
                column: "CreadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_Estado_UsuarioId",
                table: "Ventas",
                column: "Estado_UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ProductoPrincipalId",
                table: "Ventas",
                column: "ProductoPrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_UsuarioId",
                table: "Ventas",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodigosPostales");

            migrationBuilder.DropTable(
                name: "Comisiones");

            migrationBuilder.DropTable(
                name: "Contrato");

            migrationBuilder.DropTable(
                name: "DetalleVentas");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "RolUsuario");

            migrationBuilder.DropTable(
                name: "Sucursales");

            migrationBuilder.DropTable(
                name: "UsuarioRoles");

            migrationBuilder.DropTable(
                name: "Municipios");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Provincias");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Comerciales");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
