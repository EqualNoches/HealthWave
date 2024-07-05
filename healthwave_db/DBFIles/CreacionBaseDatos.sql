-- Create PerfilUsuario Table
CREATE TABLE PerfilUsuario (
    CodigoDocumento VARCHAR(30) PRIMARY KEY,
    TipoDocumento CHAR(1) NOT NULL DEFAULT 'I' CHECK (TipoDocumento IN ('I', 'P')),
    NumLicenciaMedica VARCHAR(50),
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Género CHAR(1) NOT NULL CHECK (Género IN ('M', 'F')),
    FechaNacimiento DATE NOT NULL,
    Teléfono VARCHAR(20),
    Correo VARCHAR(100),
    Dirección VARCHAR(200),
    Rol CHAR(1) NOT NULL CHECK (Rol IN ('P', 'A', 'M', 'E', 'C'))
);
GO

--Create Cuenta table
CREATE TABLE Cuenta(
    usuarioCodigo       VARCHAR(30)     PRIMARY KEY,
    documentoUsuario    VARCHAR(30)     UNIQUE      NOT NULL,   
    usuarioContra       NVARCHAR(255)   NOT NULL,
    FOREIGN KEY (documentoUsuario) REFERENCES PerfilUsuario(CodigoDocumento) ON UPDATE CASCADE ON DELETE CASCADE
);

-- Create TipoServicio Table
CREATE TABLE TipoServicio (
    TipoServicio INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL
);
GO

-- Create Aseguradora Table
CREATE TABLE Aseguradora (
    IDAseguradora INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Dirección VARCHAR(200),
    Teléfono VARCHAR(20),
    Correo VARCHAR(100)
);
GO

-- Create Consultorio Table
CREATE TABLE Consultorio (
    IDConsultorio INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Dirección VARCHAR(200),
    Teléfono VARCHAR(20)
);
GO

-- Create Autorización Table
CREATE TABLE Autorizacion (
    IDAutorizacion INT IDENTITY(1,1) PRIMARY KEY,
    FechaAutorizacion DATE NOT NULL,
    MontoAutorizado DECIMAL(10, 2) DEFAULT 0.00,
    IDAseguradora INT,
    FOREIGN KEY (IDAseguradora) REFERENCES Aseguradora(IDAseguradora) ON UPDATE CASCADE ON DELETE CASCADE
);
GO

-- Create Servicio Table
CREATE TABLE Servicio (
    ServicioCodigo INT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripción VARCHAR(200),
    TipoServicio INT,
    Costo DECIMAL(10, 2) DEFAULT 0.00,
    IDAseguradora INT,
    FOREIGN KEY (TipoServicio) REFERENCES TipoServicio(TipoServicio) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (IDAseguradora) REFERENCES Aseguradora(IDAseguradora) ON UPDATE CASCADE ON DELETE CASCADE
);
GO

-- Create Producto Table
CREATE TABLE Producto (
    IDProducto INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripción VARCHAR(200),
    Precio DECIMAL(10, 2) DEFAULT 0.00
);
GO

-- Create MetodoDePago Table
CREATE TABLE MetodoDePago (
    CodigoMetodoDePago INT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL
);
GO

-- Create Afeccion Table
CREATE TABLE Afeccion (
    IDAfeccion INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripción VARCHAR(200)
);
GO

-- Create Sala Table
CREATE TABLE Sala (
    NumSala INT IDENTITY(1,1) PRIMARY KEY,
    Estado VARCHAR(50) NOT NULL
);
GO

-- Create CuentaCobrar Table
CREATE TABLE CuentaCobrar (
    IDCuenta INT IDENTITY(1,1) PRIMARY KEY,
    Balance DECIMAL(10, 2) DEFAULT 0.00,
    Estado CHAR(1) NOT NULL CHECK (Estado IN ('A', 'D')),
    CodigoPaciente VARCHAR(30),
    FOREIGN KEY (CodigoPaciente) REFERENCES PerfilUsuario(CodigoDocumento) ON UPDATE CASCADE ON DELETE NO ACTION
);
GO

-- Create Consulta Table
CREATE TABLE [dbo].[Consulta] (
    [ConsultaCodigo]        INT             NOT NULL,
    [Fecha]                 DATE            NOT NULL,
    [Estado]                VARCHAR (50)    NOT NULL,
    [Costo]                 DECIMAL (10, 2) DEFAULT ((0.00)) NULL,
    [Motivo]                VARCHAR (200)   NULL,
    [Descripcion]           VARCHAR (200)   NULL,
    [CodigoPaciente]        VARCHAR (30)    NULL,
    [IDConsultorio]         INT             NULL,
    [IDAutorizacion]        INT             NULL,
    [CodigoDocumentoMedico] VARCHAR (30)    NULL,
    PRIMARY KEY CLUSTERED ([ConsultaCodigo] ASC),
    FOREIGN KEY ([CodigoPaciente]) REFERENCES [dbo].[PerfilUsuario] ([CodigoDocumento]) ON UPDATE CASCADE,
    FOREIGN KEY ([IDAutorizacion]) REFERENCES [dbo].[Autorizacion] ([IDAutorizacion]) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY ([IDConsultorio]) REFERENCES [dbo].[Consultorio] ([IDConsultorio]) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

-- Create Ingreso Table
CREATE TABLE Ingreso (
    IDIngreso INT IDENTITY(1,1) PRIMARY KEY,
    CostoEstancia DECIMAL(10, 2) DEFAULT 0.00,
    FechaIngreso DATE NOT NULL,
    FechaAlta DATE,
    NumSala INT,
    CodigoPaciente VARCHAR(30),
    CodigoDocumentoMedico VARCHAR(30),
    ConsultaCodigo INT,
    IDAutorizacion INT,
    FOREIGN KEY (NumSala) REFERENCES Sala(NumSala) ON UPDATE CASCADE ON DELETE NO ACTION,
    FOREIGN KEY (CodigoPaciente) REFERENCES PerfilUsuario(CodigoDocumento) ON UPDATE NO ACTION ON DELETE NO ACTION,
    FOREIGN KEY (CodigoDocumentoMedico) REFERENCES PerfilUsuario(CodigoDocumento) ON UPDATE NO ACTION ON DELETE NO ACTION,
    FOREIGN KEY (ConsultaCodigo) REFERENCES Consulta(ConsultaCodigo) ON UPDATE NO ACTION ON DELETE NO ACTION,
    FOREIGN KEY (IDAutorizacion) REFERENCES Autorizacion(IDAutorizacion) ON UPDATE CASCADE ON DELETE CASCADE
);
GO

-- Create Factura Table
CREATE TABLE Factura (
    FacturaCodigo INT PRIMARY KEY,
    MontoTotal DECIMAL(10, 2) DEFAULT 0.00,
    MontoSubtotal DECIMAL(10, 2) DEFAULT 0.00,
    Fecha DATE NOT NULL,
    RNC VARCHAR(50),
    CodigoMetodoDePago INT,
    CodigoPaciente VARCHAR(30),
    IDIngreso INT,
    IDCuenta INT,
    ConsultaCodigo INT,
    FOREIGN KEY (CodigoMetodoDePago) REFERENCES MetodoDePago(CodigoMetodoDePago) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (CodigoPaciente) REFERENCES PerfilUsuario(CodigoDocumento) ON UPDATE NO ACTION ON DELETE NO ACTION,
    FOREIGN KEY (IDIngreso) REFERENCES Ingreso(IDIngreso) ON UPDATE NO ACTION ON DELETE NO ACTION,
    FOREIGN KEY (IDCuenta) REFERENCES CuentaCobrar(IDCuenta) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (ConsultaCodigo) REFERENCES Consulta(ConsultaCodigo) ON UPDATE NO ACTION ON DELETE NO ACTION
);
GO

-- Create FacturaServicio Table
CREATE TABLE FacturaServicio (
    FacturaCodigo INT NOT NULL,
    IDProducto INT NOT NULL,
    IDAutorizacion INT NULL,
    Costo DECIMAL (10, 2) DEFAULT 0.00 NULL,
    ServicioCodigo VARCHAR (30) NULL,
    PRIMARY KEY CLUSTERED (FacturaCodigo ASC, IDProducto ASC),
    FOREIGN KEY (FacturaCodigo) REFERENCES Factura (FacturaCodigo) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (IDAutorizacion) REFERENCES Autorizacion (IDAutorizacion) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (IDProducto) REFERENCES Producto (IDProducto) ON DELETE CASCADE ON UPDATE CASCADE
);
GO

-- Create PrescripcionProducto Table
CREATE TABLE PrescripcionProducto (
    IDProducto INT,
    ConsultaCodigo INT,
    Cantidad INT,
    PRIMARY KEY (IDProducto, ConsultaCodigo),
    FOREIGN KEY (IDProducto) REFERENCES Producto(IDProducto) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (ConsultaCodigo) REFERENCES Consulta(ConsultaCodigo) ON UPDATE CASCADE ON DELETE CASCADE
);
GO

-- Create ConsultaAfeccion Table
CREATE TABLE ConsultaAfeccion (
    ConsultaCodigo INT,
    IDAfeccion INT,
    PRIMARY KEY (ConsultaCodigo, IDAfeccion),
    FOREIGN KEY (ConsultaCodigo) REFERENCES Consulta(ConsultaCodigo) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (IDAfeccion) REFERENCES Afeccion(IDAfeccion) ON UPDATE CASCADE ON DELETE CASCADE
);
GO

-- Create IngresoAfeccion Table
CREATE TABLE IngresoAfeccion (
    IDAfeccion INT,
    IDIngreso INT,
    PRIMARY KEY (IDAfeccion, IDIngreso),
    FOREIGN KEY (IDAfeccion) REFERENCES Afeccion(IDAfeccion) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (IDIngreso) REFERENCES Ingreso(IDIngreso) ON UPDATE CASCADE ON DELETE CASCADE
);
GO

-- Create ReservaServicio Table
CREATE TABLE ReservaServicio (
    ServicioCodigo INT,
    CodigoPaciente VARCHAR(30),
    PRIMARY KEY (ServicioCodigo, CodigoPaciente),
    FOREIGN KEY (ServicioCodigo) REFERENCES Servicio(ServicioCodigo) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (CodigoPaciente) REFERENCES PerfilUsuario(CodigoDocumento) ON UPDATE CASCADE ON DELETE CASCADE
);
GO

-- Create ConsultaServicio Table
CREATE TABLE ConsultaServicio (
    ConsultaCodigo INT,
    ServicioCodigo INT,
    PRIMARY KEY (ConsultaCodigo, ServicioCodigo),
    FOREIGN KEY (ConsultaCodigo) REFERENCES Consulta(ConsultaCodigo) ON UPDATE NO ACTION ON DELETE NO ACTION,
    FOREIGN KEY (ServicioCodigo) REFERENCES Servicio(ServicioCodigo) ON UPDATE NO ACTION ON DELETE NO ACTION
);
GO

-- Create FacturaProducto Table
CREATE TABLE FacturaProducto (
    FacturaCodigo INT,
    IDProducto INT,
    IDAutorizacion INT,
    Precio DECIMAL(10, 2) DEFAULT 0.00,
    Cantidad INT,
    PRIMARY KEY (FacturaCodigo, IDProducto),
    FOREIGN KEY (FacturaCodigo) REFERENCES Factura(FacturaCodigo) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (IDProducto) REFERENCES Producto(IDProducto) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (IDAutorizacion) REFERENCES Autorizacion(IDAutorizacion) ON UPDATE CASCADE ON DELETE CASCADE
);
GO

-- Create Pago Table
CREATE TABLE Pago (
    IDPago INT IDENTITY(1,1) PRIMARY KEY,
    MontoPagado DECIMAL(10, 2) DEFAULT 0.00,
    Fecha DATE NOT NULL,
    IDCuenta INT,
    FOREIGN KEY (IDCuenta) REFERENCES CuentaCobrar(IDCuenta) ON UPDATE CASCADE ON DELETE CASCADE
);
GO	
