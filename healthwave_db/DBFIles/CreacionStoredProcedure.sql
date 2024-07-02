--Stored procedures relacionados a las Autorizaciones de Seguro:
--1)Crear una nueva autorización de seguro
CREATE PROCEDURE ppAutorizacionCrear
    @FechaAutorizacion DATE,
    @MontoAutorizado DECIMAL(10, 2),
    @IDAseguradora INT
AS
BEGIN
    INSERT INTO Autorizacion (FechaAutorizacion, MontoAutorizado, IDAseguradora)
    VALUES (@FechaAutorizacion, @MontoAutorizado, @IDAseguradora);
END;
GO

--2)Actualizar una autorización de seguro existente
CREATE PROCEDURE ppAutorizacionActualizar
    @IDAutorizacion INT,
    @FechaAutorizacion DATE,
    @MontoAutorizado DECIMAL(10, 2),
    @IDAseguradora INT
AS
BEGIN
    UPDATE Autorizacion
    SET FechaAutorizacion = @FechaAutorizacion,
        MontoAutorizado = @MontoAutorizado,
        IDAseguradora = @IDAseguradora
    WHERE IDAutorizacion = @IDAutorizacion;
END;
GO

--3)Eliminar una autorización de seguro
CREATE PROCEDURE ppAutorizacionEliminar
    @IDAutorizacion INT
AS
BEGIN
    DELETE FROM Autorizacion
    WHERE IDAutorizacion = @IDAutorizacion;
END;
GO
--4)Listar todas las autorizaciones de seguro
CREATE PROCEDURE ppAutorizacionListar
    @IDAseguradora INT = NULL
AS
BEGIN
    IF @IDAseguradora IS NULL
    BEGIN
        SELECT * FROM Autorizacion;
    END
    ELSE
    BEGIN
        SELECT * FROM Autorizacion
        WHERE IDAseguradora = @IDAseguradora;
    END
END;
GO

--Stored Procedures Relacionados a las Consultas
--1)Crear una nueva consulta
CREATE PROCEDURE ppConsultaCrear
    @Fecha DATE,
    @Estado VARCHAR(50),
    @Costo DECIMAL(10, 2),
    @Motivo VARCHAR(200),
    @Descripcion VARCHAR(200),
    @CodigoPaciente VARCHAR(30),
    @IDConsultorio INT,
    @IDAutorizacion INT
AS
BEGIN
    INSERT INTO Consulta (Fecha, Estado, Costo, Motivo, Descripcion, CodigoPaciente, IDConsultorio, IDAutorizacion)
    VALUES (@Fecha, @Estado, @Costo, @Motivo, @Descripción, @CodigoPaciente, @IDConsultorio, @IDAutorizacion);
END;
GO
--2)Actualizar consulta existente
CREATE PROCEDURE ppConsultaActualizar
    @ConsultaCodigo INT,
    @Fecha DATE,
    @Estado VARCHAR(50),
    @Costo DECIMAL(10, 2),
    @Motivo VARCHAR(200),
    @Descripcion VARCHAR(200),
    @CodigoPaciente VARCHAR(30),
    @IDConsultorio INT,
    @IDAutorizacion INT
AS
BEGIN
    UPDATE Consulta
    SET Fecha = @Fecha,
        Estado = @Estado,
        Costo = @Costo,
        Motivo = @Motivo,
        Descripcion = @Descripcion,
        CodigoPaciente = @CodigoPaciente,
        IDConsultorio = @IDConsultorio,
        IDAutorizacion = @IDAutorizacion
    WHERE ConsultaCodigo = @ConsultaCodigo;
END;
GO
--3)Eliminar una consulta
CREATE PROCEDURE ppConsultaEliminar
    @ConsultaCodigo INT
AS
BEGIN
    DELETE FROM Consulta
    WHERE ConsultaCodigo = @ConsultaCodigo;
END;
GO
--4) Asociar un servicio a una consulta
CREATE PROCEDURE ppConsultaRelacionarServicio
    @ConsultaCodigo INT,
    @ServicioCodigo INT
AS
BEGIN
    INSERT INTO ConsultaServicio (ConsultaCodigo, ServicioCodigo)
    VALUES (@ConsultaCodigo, @ServicioCodigo);
END;
GO
--5)Eliminar asociacion de un servicio a una consulta
CREATE PROCEDURE ppConsultaDesrelacionarServicio
    @ConsultaCodigo INT,
    @ServicioCodigo INT
AS
BEGIN
    DELETE FROM ConsultaServicio
    WHERE ConsultaCodigo = @ConsultaCodigo AND ServicioCodigo = @ServicioCodigo;
END;
GO
--6) Obtener los servicios prescriptos en una consulta
CREATE PROCEDURE ppConsultaListarServicios
    @ConsultaCodigo INT
AS
BEGIN
    SELECT S.*
    FROM Servicio S
    INNER JOIN ConsultaServicio CS ON S.ServicioCodigo = CS.ServicioCodigo
    WHERE CS.ConsultaCodigo = @ConsultaCodigo;
END;
GO
--7)Asociar un producto a una consulta
CREATE PROCEDURE ppConsultaRelacionarProducto
    @ConsultaCodigo INT,
    @IDProducto INT,
    @Cantidad INT
AS
BEGIN
    INSERT INTO PrescripcionProducto (IDProducto, ConsultaCodigo, Cantidad)
    VALUES (@IDProducto, @ConsultaCodigo, @Cantidad);
END;
GO
--8)Eliminar asociacion de un producto a una consulta
CREATE PROCEDURE ppConsultaDesrelacionarProducto
    @ConsultaCodigo INT,
    @IDProducto INT
AS
BEGIN
    DELETE FROM PrescripcionProducto
    WHERE ConsultaCodigo = @ConsultaCodigo AND IDProducto = @IDProducto;
END;
GO
--9)Obtener los productos prescritos en una consulta
CREATE PROCEDURE ppConsultaListarProductos
    @ConsultaCodigo INT
AS
BEGIN
    SELECT P.*
    FROM Producto P
    INNER JOIN PrescripcionProducto PP ON P.IDProducto = PP.IDProducto
    WHERE PP.ConsultaCodigo = @ConsultaCodigo;
END;
GO
--10)Asociar una afeccion a una consulta
CREATE PROCEDURE ppConsultaRelacionarAfeccion
    @ConsultaCodigo INT,
    @IDAfeccion INT
AS
BEGIN
    INSERT INTO ConsultaAfeccion (ConsultaCodigo, IDAfeccion)
    VALUES (@ConsultaCodigo, @IDAfeccion);
END;
GO
--11)Eliminar asociacion de una afeccion a una consulta
CREATE PROCEDURE ppConsultaDesrelacionarAfeccion
    @ConsultaCodigo INT,
    @IDAfeccion INT
AS
BEGIN
    DELETE FROM ConsultaAfeccion
    WHERE ConsultaCodigo = @ConsultaCodigo AND IDAfeccion = @IDAfeccion;
END;
GO
--12)Obtener afecciones tratadas en una consulta
CREATE PROCEDURE ppConsultaListarAfecciones
    @ConsultaCodigo INT
AS
BEGIN
    SELECT A.*
    FROM Afeccion A
    INNER JOIN ConsultaAfeccion CA ON A.IDAfeccion = CA.IDAfeccion
    WHERE CA.ConsultaCodigo = @ConsultaCodigo;
END;
GO
--13)Listar las consultas aplicando filtros 
CREATE PROCEDURE ppConsultasListar
    @DocumentoPaciente VARCHAR(30) = NULL,
    @DocumentoMedico VARCHAR(30) = NULL,
    @FechaInicio DATE = NULL,
    @FechaFin DATE = NULL
AS
BEGIN
    SELECT * FROM Consulta
    WHERE (@DocumentoPaciente IS NULL OR CodigoPaciente = @DocumentoPaciente)
      AND (@DocumentoMedico IS NULL OR CodigoDocumentoMedico = @DocumentoMedico)
      AND (@FechaInicio IS NULL OR Fecha >= @FechaInicio)
      AND (@FechaFin IS NULL OR Fecha <= @FechaFin);
END;
GO

--Stored Procedures relacionados a la creación de facturas:
--1)Para crear factura asociada a una consulta
CREATE PROCEDURE ppFacturaCrearConsulta
    @FacturaCodigo VARCHAR(30),
    @MontoTotal DECIMAL(10, 2),
    @MontoSubtotal DECIMAL(10, 2),
    @Fecha DATE,
    @RNC VARCHAR(50),
    @CodigoMetodoDePago INT,
    @CodigoPaciente VARCHAR(30),
    @ConsultaCodigo INT
AS
BEGIN
    INSERT INTO Factura (FacturaCodigo, MontoTotal, MontoSubtotal, Fecha, RNC, CodigoMetodoDePago, CodigoPaciente, ConsultaCodigo)
    VALUES (@FacturaCodigo, @MontoTotal, @MontoSubtotal, @Fecha, @RNC, @CodigoMetodoDePago, @CodigoPaciente, @ConsultaCodigo);
END;
GO

--2)Para generar facturas asociada a un ingreso
CREATE PROCEDURE ppFacturaCrearIngreso
    @FacturaCodigo VARCHAR(30),
    @MontoTotal DECIMAL(10, 2),
    @MontoSubtotal DECIMAL(10, 2),
    @Fecha DATE,
    @RNC VARCHAR(50),
    @CodigoMetodoDePago INT,
    @CodigoPaciente VARCHAR(30),
    @IDIngreso INT
AS
BEGIN
    INSERT INTO Factura (FacturaCodigo, MontoTotal, MontoSubtotal, Fecha, RNC, CodigoMetodoDePago, CodigoPaciente, IDIngreso)
    VALUES (@FacturaCodigo, @MontoTotal, @MontoSubtotal, @Fecha, @RNC, @CodigoMetodoDePago, @CodigoPaciente, @IDIngreso);
END;
GO

--3)Para generar facturas
CREATE PROCEDURE ppFacturaCrear
    @FacturaCodigo VARCHAR(30),
    @MontoTotal DECIMAL(10, 2),
    @MontoSubtotal DECIMAL(10, 2),
    @Fecha DATE,
    @RNC VARCHAR(50),
    @CodigoMetodoDePago INT,
    @CodigoPaciente VARCHAR(30)
AS
BEGIN
    INSERT INTO Factura (FacturaCodigo, MontoTotal, MontoSubtotal, Fecha, RNC, CodigoMetodoDePago, CodigoPaciente)
    VALUES (@FacturaCodigo, @MontoTotal, @MontoSubtotal, @Fecha, @RNC, @CodigoMetodoDePago, @CodigoPaciente);
END;
GO

--Stored Procedures relacionados a la gestión de facturas:
--1)Actualizar factura
CREATE PROCEDURE ppFacturaActualizar
    @FacturaCodigo VARCHAR(30),
    @MontoTotal DECIMAL(10, 2),
    @MontoSubtotal DECIMAL(10, 2),
    @Fecha DATE,
    @RNC VARCHAR(50),
    @CodigoMetodoDePago INT,
    @CodigoPaciente VARCHAR(30),
    @IDIngreso INT = NULL,
    @ConsultaCodigo INT = NULL
AS
BEGIN
    UPDATE Factura
    SET MontoTotal = @MontoTotal,
        MontoSubtotal = @MontoSubtotal,
        Fecha = @Fecha,
        RNC = @RNC,
        CodigoMetodoDePago = @CodigoMetodoDePago,
        CodigoPaciente = @CodigoPaciente,
        IDIngreso = @IDIngreso,
        ConsultaCodigo = @ConsultaCodigo
    WHERE FacturaCodigo = @FacturaCodigo;
END;
GO

--2)Eliminar facturas
CREATE PROCEDURE ppFacturaEliminar
    @FacturaCodigo VARCHAR(30)
AS
BEGIN
    DELETE FROM Factura WHERE FacturaCodigo = @FacturaCodigo;
END;
GO

--3)Listar todas las facturas generadas con filtros
CREATE PROCEDURE ppFacturaListar
    @DocumentoCajero VARCHAR(30) = NULL,
    @FechaInicio DATE = NULL,
    @FechaFin DATE = NULL
AS
BEGIN
    SELECT * FROM Factura
    WHERE (@DocumentoCajero IS NULL OR CodigoPaciente = @DocumentoCajero)
      AND (@FechaInicio IS NULL OR Fecha >= @FechaInicio)
      AND (@FechaFin IS NULL OR Fecha <= @FechaFin);
END;
GO

--Stored Procedures relacionados a la asociación de servicios y productos a facturas:
--1)Generar las facturas asociadas a los servicios
CREATE PROCEDURE ppFacturaRelacionarServicio
    @FacturaCodigo VARCHAR(30),
    @ServicioCodigo INT,
    @Costo DECIMAL(10, 2)
AS
BEGIN
    INSERT INTO FacturaServicio (FacturaCodigo, ServicioCodigo, Costo)
    VALUES (@FacturaCodigo, @ServicioCodigo, @Costo);
END;
GO

--2)Eliminar las facturas asociadas a los servicios
CREATE PROCEDURE ppFacturaDesrelacionarServicio
    @FacturaCodigo VARCHAR(30),
    @ServicioCodigo INT
AS
BEGIN
    DELETE FROM FacturaServicio
    WHERE FacturaCodigo = @FacturaCodigo AND ServicioCodigo = @ServicioCodigo;
END;
GO

--3)Listar todas las facturas asciadas a los servicios
CREATE PROCEDURE ppFacturaListarServicios
    @FacturaCodigo VARCHAR(30)
AS
BEGIN
    SELECT * FROM FacturaServicio
    WHERE FacturaCodigo = @FacturaCodigo;
END;
GO

--4)Facturas asociadas a los productos
CREATE PROCEDURE ppFacturaRelacionarProducto
    @FacturaCodigo VARCHAR(30),
    @IDProducto INT,
    @Precio DECIMAL(10, 2),
    @Cantidad INT
AS
BEGIN
    INSERT INTO FacturaProducto (FacturaCodigo, IDProducto, Precio, Cantidad)
    VALUES (@FacturaCodigo, @IDProducto, @Precio, @Cantidad);
END;
GO

--5)Eliminar las facturas asociadas a los productos
CREATE PROCEDURE ppFacturaDesrelacionarProducto
    @FacturaCodigo VARCHAR(30),
    @IDProducto INT
AS
BEGIN
    DELETE FROM FacturaProducto
    WHERE FacturaCodigo = @FacturaCodigo AND IDProducto = @IDProducto;
END;
GO

--6)Listar todas las facturas asociadas a los productos
CREATE PROCEDURE ppFacturaListarProductos
    @FacturaCodigo VARCHAR(30)
AS
BEGIN
    SELECT * FROM FacturaProducto
    WHERE FacturaCodigo = @FacturaCodigo;
END;
GO
