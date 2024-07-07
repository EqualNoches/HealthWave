
-- 1. Crear tabla Afeccion
CREATE TABLE Afeccion (
    IDAfeccion  INT           IDENTITY (1, 1) NOT NULL,
    Nombre      VARCHAR (100) NOT NULL,
    Descripcion VARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED (IDAfeccion ASC)
);

-- 2. Crear tabla Aseguradora
CREATE TABLE Aseguradora (
    IDAseguradora INT           IDENTITY (1, 1) NOT NULL,
    Nombre        VARCHAR (100) NOT NULL,
    Dirección     VARCHAR (200) NULL,
    Telefono      VARCHAR (20)  NULL,
    Correo        VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED (IDAseguradora ASC)
);

-- 3. Crear tabla Autorizacion
CREATE TABLE Autorizacion (
    IDAutorizacion    INT             IDENTITY (1, 1) NOT NULL,
    FechaAutorizacion DATE            NOT NULL,
    MontoAutorizado   DECIMAL (10, 2) DEFAULT ((0.00)) NULL,
    IDAseguradora     INT             NULL,
    PRIMARY KEY CLUSTERED (IDAutorizacion ASC),
    FOREIGN KEY (IDAseguradora) REFERENCES Aseguradora (IDAseguradora) ON DELETE CASCADE ON UPDATE CASCADE
);

-- 4. Crear tabla Consultorio
CREATE TABLE Consultorio (
    IDConsultorio INT           IDENTITY (1, 1) NOT NULL,
    Nombre        VARCHAR (100) NOT NULL,
    Direccion     VARCHAR (200) NULL,
    Telefono      VARCHAR (20)  NULL,
    PRIMARY KEY CLUSTERED (IDConsultorio ASC)
);

-- 5. Crear tabla PerfilUsuario
CREATE TABLE PerfilUsuario (
    CodigoDocumento   VARCHAR (30)  NOT NULL,
    TipoDocumento     CHAR (1)      DEFAULT ('I') NOT NULL,
    NumLicenciaMedica VARCHAR (50)  NULL,
    Nombre            VARCHAR (100) NOT NULL,
    Apellido          VARCHAR (100) NOT NULL,
    Genero            CHAR (1)      NOT NULL,
    FechaNacimiento   DATE          NOT NULL,
    Telefono          VARCHAR (20)  NULL,
    Correo            VARCHAR (100) NULL,
    Direccion         VARCHAR (200) NULL,
    Rol               CHAR (1)      NOT NULL,
    PRIMARY KEY CLUSTERED (CodigoDocumento ASC),
    CHECK (Genero='F' OR Genero='M'),
    CHECK (Rol='C' OR Rol='E' OR Rol='M' OR Rol='A' OR Rol='P'),
    CHECK (TipoDocumento='P' OR TipoDocumento='I')
);

-- 6. Crear tabla Consulta
CREATE TABLE Consulta (
    ConsultaCodigo        INT             NOT NULL,
    Fecha                 DATE            NOT NULL,
    Estado                VARCHAR (50)    NOT NULL,
    Costo                 DECIMAL (10, 2) DEFAULT ((0.00)) NULL,
    Motivo                VARCHAR (200)   NULL,
    Descripcion           VARCHAR (200)   NULL,
    CodigoPaciente        VARCHAR (30)    NULL,
    IDConsultorio         INT             NULL,
    IDAutorizacion        INT             NULL,
    CodigoDocumentoMedico VARCHAR (30)    NULL,
    PRIMARY KEY CLUSTERED (ConsultaCodigo ASC),
    FOREIGN KEY (CodigoPaciente) REFERENCES PerfilUsuario (CodigoDocumento) ON UPDATE CASCADE,
    FOREIGN KEY (IDAutorizacion) REFERENCES Autorizacion (IDAutorizacion) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (IDConsultorio) REFERENCES Consultorio (IDConsultorio) ON DELETE CASCADE ON UPDATE CASCADE
);

-- 7. Crear tabla ConsultaAfeccion
CREATE TABLE ConsultaAfeccion (
    ConsultaCodigo INT NOT NULL,
    IDAfeccion     INT NOT NULL,
    PRIMARY KEY CLUSTERED (ConsultaCodigo ASC, IDAfeccion ASC),
    FOREIGN KEY (ConsultaCodigo) REFERENCES Consulta (ConsultaCodigo) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (IDAfeccion) REFERENCES Afeccion (IDAfeccion) ON DELETE CASCADE ON UPDATE CASCADE
);

-- 8. Crear tabla TipoServicio
CREATE TABLE TipoServicio (
    IDTipoServicio INT           IDENTITY (1, 1) NOT NULL,
    Nombre         VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED (IDTipoServicio ASC)
);

-- 9. Crear tabla Servicio
CREATE TABLE Servicio (
    ServicioCodigo INT             NOT NULL,
    Nombre         VARCHAR (100)   NOT NULL,
    Descripcion    VARCHAR (200)   NULL,
    IDTipoServicio INT             NULL,
    Costo          DECIMAL (10, 2) DEFAULT ((0.00)) NULL,
    IDAseguradora  INT             NULL,
    PRIMARY KEY CLUSTERED (ServicioCodigo ASC),
    FOREIGN KEY (IDAseguradora) REFERENCES Aseguradora (IDAseguradora) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (IDTipoServicio) REFERENCES TipoServicio (IDTipoServicio) ON DELETE CASCADE ON UPDATE CASCADE
);

-- 10. Crear tabla ConsultaServicio
CREATE TABLE ConsultaServicio (
    ConsultaCodigo INT NOT NULL,
    ServicioCodigo INT NOT NULL,
    PRIMARY KEY CLUSTERED (ConsultaCodigo ASC, ServicioCodigo ASC),
    FOREIGN KEY (ConsultaCodigo) REFERENCES Consulta (ConsultaCodigo),
    FOREIGN KEY (ServicioCodigo) REFERENCES Servicio (ServicioCodigo)
);

-- 11. Crear tabla CuentaCobrar
CREATE TABLE CuentaCobrar (
    IDCuenta       INT             IDENTITY (1, 1) NOT NULL,
    Balance        DECIMAL (10, 2) DEFAULT ((0.00)) NULL,
    Estado         CHAR (1)        NOT NULL,
    CodigoPaciente VARCHAR (30)    NULL,
    PRIMARY KEY CLUSTERED (IDCuenta ASC),
    CHECK (Estado='D' OR Estado='A'),
    FOREIGN KEY (CodigoPaciente) REFERENCES PerfilUsuario (CodigoDocumento) ON UPDATE CASCADE
);

-- 12. Crear tabla Sala
CREATE TABLE Sala (
    NumSala INT          IDENTITY (1, 1) NOT NULL,
    Estado  VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED (NumSala ASC)
);

-- 13. Crear tabla Ingreso
CREATE TABLE Ingreso (
    IDIngreso             INT             IDENTITY (1, 1) NOT NULL,
    CostoEstancia         DECIMAL (10, 2) DEFAULT ((0.00)) NULL,
    FechaIngreso          DATE            NOT NULL,
    FechaAlta             DATE            NULL,
    NumSala               INT             NULL,
    CodigoPaciente        VARCHAR (30)    NULL,
    CodigoDocumentoMedico VARCHAR (30)    NULL,
    ConsultaCodigo        INT             NULL,
    IDAutorizacion        INT             NULL,
    PRIMARY KEY CLUSTERED (IDIngreso ASC),
    FOREIGN KEY (CodigoDocumentoMedico) REFERENCES PerfilUsuario (CodigoDocumento),
    FOREIGN KEY (CodigoPaciente) REFERENCES PerfilUsuario (CodigoDocumento),
    FOREIGN KEY (ConsultaCodigo) REFERENCES Consulta (ConsultaCodigo),
    FOREIGN KEY (IDAutorizacion) REFERENCES Autorizacion (IDAutorizacion) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (NumSala) REFERENCES Sala (NumSala) ON UPDATE CASCADE
);

-- 14. Crear tabla MetodoDePago
CREATE TABLE MetodoDePago (
    CodigoMetodoDePago INT           NOT NULL,
    Nombre             VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED (CodigoMetodoDePago ASC)
);

-- 15. Crear tabla Factura
CREATE TABLE Factura (
    FacturaCodigo      VARCHAR (30)    NOT NULL,
    MontoTotal         DECIMAL (10, 2) DEFAULT ((0.00)) NULL,
    MontoSubtotal      DECIMAL (10, 2) DEFAULT ((0.00)) NULL,
    Fecha              DATE            NOT NULL,
    RNC                VARCHAR (50)    NULL,
    CodigoMetodoDePago INT             NULL,
    CodigoPaciente     VARCHAR (30)    NULL,
    IDIngreso          INT             NULL,
    IDCuenta           INT             NULL,
    ConsultaCodigo     INT             NULL,
    PRIMARY KEY CLUSTERED (FacturaCodigo ASC),
    FOREIGN KEY (CodigoMetodoDePago) REFERENCES MetodoDePago (CodigoMetodoDePago) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (CodigoPaciente) REFERENCES PerfilUsuario (CodigoDocumento),
    FOREIGN KEY (ConsultaCodigo) REFERENCES Consulta (ConsultaCodigo),
    FOREIGN KEY (IDCuenta) REFERENCES CuentaCobrar (IDCuenta) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (IDIngreso) REFERENCES Ingreso (IDIngreso)
);

-- 16. Crear tabla Producto
CREATE TABLE Producto (
    IDProducto  INT             IDENTITY (1, 1) NOT NULL,
    Nombre      VARCHAR (100)   NOT NULL,
    Descripcion VARCHAR (200)   NULL,
    Precio      DECIMAL (10, 2) DEFAULT ((0.00)) NULL,
    PRIMARY KEY CLUSTERED (IDProducto ASC)
);

-- 17. Crear tabla FacturaProducto
CREATE TABLE FacturaProducto (
    FacturaCodigoProducto VARCHAR (30)    NOT NULL,
    IDProducto            INT             NOT NULL,
    IDAutorizacion        INT             NULL,
    Precio                DECIMAL (10, 2) DEFAULT ((0.00)) NULL,
    Cantidad              INT             NULL,
    PRIMARY KEY CLUSTERED (FacturaCodigoProducto ASC, IDProducto ASC),
    FOREIGN KEY (FacturaCodigoProducto) REFERENCES Factura (FacturaCodigo) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (IDAutorizacion) REFERENCES Autorizacion (IDAutorizacion) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (IDProducto) REFERENCES Producto (IDProducto) ON DELETE CASCADE ON UPDATE CASCADE
);

-- 18. Crear tabla FacturaServicio
CREATE TABLE FacturaServicio (
    FacturaCodigoServicio VARCHAR (30)    NOT NULL,
    IDProducto            INT             NOT NULL,
    IDAutorizacion        INT             NULL,
    Costo                 DECIMAL (10, 2) DEFAULT ((0.00)) NULL,
    ServicioCodigo        VARCHAR (30)    NULL,
    PRIMARY KEY CLUSTERED (FacturaCodigoServicio ASC, IDProducto ASC),
    FOREIGN KEY (FacturaCodigoServicio) REFERENCES Factura (FacturaCodigo) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (IDAutorizacion) REFERENCES Autorizacion (IDAutorizacion) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (IDProducto) REFERENCES Producto (IDProducto) ON DELETE CASCADE ON UPDATE CASCADE
);

-- 19. Crear tabla IngresoAfeccion
CREATE TABLE IngresoAfeccion (
    IDIngreso  INT NOT NULL,
    IDAfeccion INT NOT NULL,
    PRIMARY KEY CLUSTERED (IDAfeccion ASC, IDIngreso ASC),
    FOREIGN KEY (IDAfeccion) REFERENCES Afeccion (IDAfeccion) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (IDIngreso) REFERENCES Ingreso (IDIngreso) ON DELETE CASCADE ON UPDATE CASCADE
);

-- 20. Crear tabla Pago
CREATE TABLE Pago (
    IDPago      INT             IDENTITY (1, 1) NOT NULL,
    MontoPagado DECIMAL (10, 2) DEFAULT ((0.00)) NULL,
    Fecha       DATE            NOT NULL,
    IDCuenta    INT             NULL,
    PRIMARY KEY CLUSTERED (IDPago ASC),
    FOREIGN KEY (IDCuenta) REFERENCES CuentaCobrar (IDCuenta) ON DELETE CASCADE ON UPDATE CASCADE
);

-- 21. Crear tabla PrescripcionProducto
CREATE TABLE PrescripcionProducto (
    IDProducto     INT NOT NULL,
    ConsultaCodigo INT NOT NULL,
    Cantidad       INT NULL,
    PRIMARY KEY CLUSTERED (IDProducto ASC, ConsultaCodigo ASC),
    FOREIGN KEY (ConsultaCodigo) REFERENCES Consulta (ConsultaCodigo) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (IDProducto) REFERENCES Producto (IDProducto) ON DELETE CASCADE ON UPDATE CASCADE
);

-- 22. Crear tabla ReservaServicio
CREATE TABLE ReservaServicio (
    ServicioCodigo INT          NOT NULL,
    CodigoPaciente VARCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED (ServicioCodigo ASC, CodigoPaciente ASC),
    FOREIGN KEY (CodigoPaciente) REFERENCES PerfilUsuario (CodigoDocumento) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (ServicioCodigo) REFERENCES Servicio (ServicioCodigo) ON DELETE CASCADE ON UPDATE CASCADE
);

-- 23. Crear tabla Usuario
CREATE TABLE Usuario (
    usuarioCodigo    VARCHAR (30)   NOT NULL,
    documentoUsuario VARCHAR (30)   NOT NULL,
    usuarioContra    NVARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED (usuarioCodigo ASC),
    FOREIGN KEY (documentoUsuario) REFERENCES PerfilUsuario (CodigoDocumento) ON DELETE CASCADE ON UPDATE CASCADE,
    UNIQUE NONCLUSTERED (documentoUsuario ASC)
);
