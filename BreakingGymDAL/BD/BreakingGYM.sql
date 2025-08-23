CREATE DATABASE BreakingGymADWA6;
GO
USE BreakingGymADWA6;
GO
---------------TABLAS----------------------------
----
CREATE TABLE Tarjeta(
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Numerorfid VARCHAR(50) NOT NULL
);
GO
--ROL
CREATE TABLE Rol (
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50) NOT NULL
);
GO
--SERVICIO
CREATE TABLE Servicio(
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50) NOT NULL,
Descripcion VARCHAR(MAX) NOT NULL
);
GO
----ESTADO
CREATE TABLE Estado (
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50) NOT NULL
);
GO
CREATE TABLE TipoDocumento (
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(50) NOT NULL
);
GO

-----USUARIO
CREATE TABLE Usuario (
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
IdRol INT NOT NULL FOREIGN KEY REFERENCES Rol(Id),
Nombre VARCHAR(50) NOT NULL,
Apellido VARCHAR(50) NOT NULL,
Celular VARCHAR(9) NOT NULL,
Cuenta VARCHAR(50) NOT NULL,
Contrasenia VARCHAR(20) NOT NULL,
);
GO

---CLIENTE
CREATE TABLE Cliente (
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    IdRol INT NOT NULL FOREIGN KEY REFERENCES Rol(Id),
    IdTarjeta INT NULL FOREIGN KEY REFERENCES Tarjeta(Id),
    IdTipoDocumento INT NOT NULL FOREIGN KEY REFERENCES TipoDocumento(Id),
    Documento VARCHAR(50) NOT NULL,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Celular VARCHAR(9) NOT NULL
);
GO
---MEMBRESIA
CREATE TABLE Membresia (
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(30) NOT NULL,
IdServicio INT NOT NULL FOREIGN KEY REFERENCES Servicio(Id),
Precio INT NOT NULL,
Duracion VARCHAR(10) NOT NULL,
Descripcion VARCHAR(MAX) NOT NULL
);
GO
---INSCRIPCION
CREATE TABLE Inscripcion (
Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
IdCliente INT NOT NULL FOREIGN KEY REFERENCES Cliente(Id),
IdMembresia INT NOT NULL FOREIGN KEY REFERENCES Membresia(Id),
IdEstado INT NOT NULL FOREIGN KEY REFERENCES Estado(Id),
FechaInscripcion DATE NOT NULL,
FechaVencimiento DATE NOT NULL
);
GO
CREATE TABLE RegistroAsistencia (
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    IdCliente INT NOT NULL FOREIGN KEY REFERENCES Cliente(Id),
	IdTarjeta INT NOT NULL FOREIGN KEY REFERENCES Tarjeta(Id),
    NombreCliente VARCHAR(50) NOT NULL,
    ApellidoCliente VARCHAR(50) NOT NULL,
    DocumentoCliente VARCHAR(50) NOT NULL,
    FechaAsistencia DATE NOT NULL,
    HoraAsistencia TIME NOT NULL
);
GO
----TARJETARFID
CREATE PROCEDURE GuardarTarjeta
@Numerorfid VARCHAR(50)=NULL
AS 
BEGIN
INSERT INTO Tarjeta(Numerorfid)
VALUES(@Numerorfid);
END
GO
CREATE PROCEDURE EliminarTarjeta
@Id INT=NULL
AS 
BEGIN
DELETE FROM Tarjeta
WHERE Id=@Id;
END
GO

CREATE PROCEDURE MostrarTarjeta
    @Numerorfid VARCHAR(50)=NULL
	
AS
BEGIN
    SELECT 
        Id,
        Numerorfid
		
    FROM 
        Tarjeta
    WHERE
	 (@Numerorfid IS NULL OR Numerorfid LIKE '%' + @Numerorfid + '%')
		
END
GO
GO

CREATE PROCEDURE ModificarTarjeta
@Id INT=NULL,
@Numerorfid VARCHAR(50)=NULL
AS
BEGIN
UPDATE Tarjeta
SET
Numerorfid=@Numerorfid
WHERE Id=@Id;
END
GO
----------------PROCESOS ALMACENADOS-----------------------------
CREATE PROCEDURE GuardarTipoDocumento
	@Nombre	VARCHAR(50)=NULL
AS
BEGIN
	INSERT INTO TipoDocumento (Nombre)
VALUES(@Nombre);
END
GO
CREATE PROCEDURE EliminarTipoDocumento
    @Id INT=NULL
AS 
BEGIN
 DELETE FROM TipoDocumento
 WHERE Id = @Id;
END
GO
CREATE PROCEDURE MostrarTipoDocumento
    @Nombre VARCHAR(50) =NULL
AS
BEGIN
    SELECT 
        Id,
        Nombre
    FROM 
        TipoDocumento
    WHERE
     (@Nombre IS NULL OR Nombre LIKE '%' + @Nombre + '%')
		
END
GO
CREATE PROCEDURE ModificarTipoDocumento
    @Id INT=NULL,
	@Nombre VARCHAR(50)=NULL
AS
BEGIN
	UPDATE TipoDocumento
    SET
	  Nombre = @Nombre
	WHERE Id = @Id;
END
GO
----ESTADO
CREATE PROCEDURE GuardarEstado
	@Nombre	VARCHAR(50)=NULL
AS
BEGIN
	INSERT INTO Estado (Nombre)
VALUES(@Nombre);
END
GO

------Elimina mediante el Id---------
CREATE PROCEDURE EliminarEstado
    @Id INT=NULL
AS 
BEGIN
 DELETE FROM Estado
 WHERE Id = @Id;
END
GO
CREATE PROCEDURE ModificarEstado
    @Id INT=NULL,
	@Nombre VARCHAR(50)=NULL
AS
BEGIN
	UPDATE Estado
    SET
	  Nombre = @Nombre
	WHERE Id = @Id;
END
GO
CREATE PROCEDURE MostrarEstado
    @Nombre VARCHAR(50) =NULL
AS
BEGIN
    SELECT 
        Id,
        Nombre
    FROM 
        Estado
    WHERE
     (@Nombre IS NULL OR Nombre LIKE '%' + @Nombre + '%')
		
END
GO
---INSCRIPCION
CREATE PROCEDURE GuardarInscripcion
    @IdCliente INT=NULL,
    @IdMembresia INT=NULL,
    @IdEstado INT=NULL,
    @FechaInscripcion DATE=NULL,
    @FechaVencimiento DATE=NULL
AS
BEGIN
	INSERT INTO Inscripcion (IdCliente,IdMembresia,IdEstado,FechaInscripcion,FechaVencimiento)
VALUES (@IdCliente,@IdMembresia,@IdEstado,@FechaInscripcion,@FechaVencimiento);
END
GO
CREATE PROCEDURE EliminarInscripcion
    @Id INT=NULL
AS 
BEGIN
 DELETE FROM Inscripcion 
 WHERE Id = @Id;
END
GO
CREATE PROCEDURE ModificarInscripcion
    @Id INT=NULL,
    @IdCliente INT=NULL,
    @IdMembresia INT=NULL,
    @IdEstado INT=NULL,
    @FechaInscripcion DATE=NULL,
    @FechaVencimiento DATE=NULL
AS
BEGIN
	UPDATE Inscripcion
	SET 
	   @IdCliente = @IdCliente,
	   IdMembresia = @IdMembresia,
	   IdEstado = @IdEstado,
	   FechaInscripcion = @FechaInscripcion,
	   FechaVencimiento = @FechaVencimiento
    WHERE Id = @id;
END
GO
CREATE PROCEDURE MostrarInscripcion
    @IdCliente INT=NULL,
    @IdMembresia INT=NULL,
    @IdEstado INT=NULL,
    @FechaInscripcion DATE=NULL,
    @FechaVencimiento DATE=NULL
AS
BEGIN
    SELECT 
        Id,
        IdCliente,
		IdMembresia,
		IdEstado,
		FechaInscripcion,
		FechaVencimiento
    FROM 
        Inscripcion
    WHERE
	    (@IdCliente IS NULL OR IdCliente =  @IdCliente )
	    AND (@IdMembresia IS NULL OR IdMembresia =  @IdMembresia )
        AND (@IdEstado IS NULL OR IdEstado = @IdEstado)
		AND (@FechaInscripcion IS NULL OR FechaInscripcion = @FechaInscripcion)
        AND (@FechaVencimiento IS NULL OR FechaVencimiento = @FechaVencimiento)
END

GO
---MEMBRESIA
CREATE PROCEDURE GuardarMembresia
	@Nombre	VARCHAR(30)=NULL,
	@IdServicio INT=NULL,
	@Precio INT=NULL,
	@Duracion VARCHAR(10)=NULL,
	@Descripcion VARCHAR(MAX)=NULL
AS
BEGIN
	INSERT INTO Membresia (Nombre, IdServicio, Precio, Duracion, Descripcion)
VALUES(@Nombre,@IdServicio,@Precio,@Duracion,@Descripcion);
END
GO
CREATE PROCEDURE EliminarMembresia
    @Id INT=NULL
AS 
BEGIN
 DELETE FROM Membresia
 WHERE Id = @Id;
END
GO
CREATE PROCEDURE ModificarMembresia
    @Id INT=NULL,
    @Nombre VARCHAR(30)=NULL,
    @IdServicio INT=NULL,
    @Precio INT=NULL,
	@Duracion VARCHAR(10)=NULL,
	@Descripcion VARCHAR(MAX)=NULL
AS
BEGIN
	UPDATE Membresia
	SET 
	   Nombre = @Nombre,
	   IdServicio = @IdServicio,
	   Precio = @Precio,
	   Duracion = @Duracion,
	   Descripcion = @Descripcion
    WHERE Id = @id;
END
GO
CREATE PROCEDURE MostrarMembresia
    @Nombre VARCHAR(30)=NULL,
    @IdServicio INT=NULL,
    @Precio INT=NULL,
	@Duracion VARCHAR(10)=NULL,
	@Descripcion VARCHAR(MAX)=NULL
AS
BEGIN
    SELECT 
        Id,
        Nombre,
		IdServicio,
		Precio,
		Duracion,
		Descripcion

    FROM 
        Membresia
    WHERE
	    (@Nombre IS NULL OR Nombre LIKE '%' + @Nombre + '%')
	    AND (@IdServicio IS NULL OR IdServicio =  @IdServicio)
		AND (@Precio IS NULL OR Precio = @Precio)
        AND (@Duracion IS NULL OR Duracion LIKE '%' + @Duracion + '%')
		AND (@Descripcion IS NULL OR Descripcion LIKE '%' + @Descripcion + '%')
END
GO
CREATE PROCEDURE ObtenerMembresiaPorId
    @Id INT
AS
BEGIN
    SELECT Id, IdServicio, Nombre, Duracion, Precio, Descripcion
    FROM Membresia
    WHERE Id = @Id
END
GO
---ROL
CREATE PROCEDURE GuardarRol
	@Nombre VARCHAR(50)=NULL
AS
BEGIN
	INSERT INTO Rol (Nombre)
VALUES(@Nombre);
END
GO
CREATE PROCEDURE EliminarRol
	@Id INT=NULL
AS
BEGIN
	DELETE FROM Rol
 WHERE Id = @Id;
END
GO
CREATE PROCEDURE ModificarRol
    @Id INT=NULL,
	@Nombre VARCHAR(50)=NULL
AS
BEGIN
	UPDATE Rol
    SET
	  Nombre = @Nombre
	WHERE Id = @Id;
END
GO
CREATE PROCEDURE MostrarRol
    @Nombre VARCHAR(50)=NULL
AS
BEGIN
    SELECT 
        Id,
        Nombre
    FROM 
        Rol
    WHERE
     (@Nombre IS NULL OR Nombre LIKE '%' + @Nombre + '%')		
END
GO
CREATE PROCEDURE ObtenerRoles
AS
BEGIN
    SELECT Id, Nombre FROM Rol;
END
GO
-----SERVICIO
CREATE PROCEDURE GuardarServicio
	@Nombre	VARCHAR(50)=NULL,
	@Descripcion TEXT=NULL
AS
BEGIN
	INSERT INTO Servicio (Nombre, Descripcion)
VALUES(@Nombre,@Descripcion);
END
GO
CREATE PROCEDURE EliminarServicio
    @Id INT =NULL
AS 
BEGIN
 DELETE FROM Servicio
 WHERE Id = @Id;
END
GO
CREATE PROCEDURE ModificarServicio
    @Id INT=NULL,
	@Nombre VARCHAR(50)=NULL,
	@Descripcion VARCHAR(MAX)=NULL
AS
BEGIN
	UPDATE Servicio
    SET
	  Nombre = @Nombre,
	  Descripcion = @Descripcion
	WHERE Id = @Id;
END
GO
CREATE PROCEDURE MostrarServicio
    @Nombre VARCHAR(50)=NULL,
	@Descripcion VARCHAR(MAX)=NULL
AS
BEGIN
    SELECT 
        Id,
        Nombre,
		Descripcion 
    FROM 
        Servicio
    WHERE
	 (@Nombre IS NULL OR Nombre LIKE '%' + @Nombre + '%')
    AND (@Descripcion IS NULL OR Descripcion LIKE '%' + @Descripcion + '%')
		
END
GO
----USUARIO----

CREATE PROCEDURE GuardarUsuario
    @IdRol INT=NULL,
	@Nombre	VARCHAR(50)=NULL,
	@Apellido VARCHAR(50)=NULL,
	@Celular VARCHAR(9)=NULL,
	@Cuenta VARCHAR(50)=NULL,
	@Contrasenia VARCHAR(20)=NULL
	
AS
BEGIN
	INSERT INTO Usuario (IdRol, Nombre, Apellido, Celular, Cuenta, Contrasenia)
VALUES(@IdRol,@Nombre,@Apellido,@Celular,@Cuenta,@Contrasenia);
END
GO
CREATE PROCEDURE EliminarUsuario
    @Id INT=NULL
AS 
BEGIN
 DELETE FROM Usuario
 WHERE Id = @Id;
END
GO
CREATE PROCEDURE ModificarUsuario
    @Id INT=NULL,
	@IdRol INT=NULL,
    @Nombre VARCHAR(50)=NULL,
    @Apellido VARCHAR(50)=NULL,
    @Celular VARCHAR(9)=NULL,
    @Cuenta VARCHAR(50)=NULL,
	@Contrasenia VARCHAR(20)=NULL
AS
BEGIN
	UPDATE Usuario
    SET
	  IdRol = @IdRol,
	  Nombre = @Nombre,
	  Apellido = @Apellido,
	  Celular = @Celular,
	  Cuenta = @Cuenta,
	  Contrasenia = @Contrasenia
	WHERE Id = @Id;
END
GO
CREATE PROCEDURE MostrarUsuario
    @IdRol INT= NULL,
    @Nombre VARCHAR(50)=NULL,
    @Apellido VARCHAR(50)=NULL,
    @Celular VARCHAR(9)=NULL,
	@Cuenta VARCHAR(50)=NULL,
	@Contrasenia VARCHAR(20)=NULL
AS
BEGIN
    SELECT 
        Id,
		IdRol,
        Nombre,
		Apellido,
		Celular,
		Cuenta,
		Contrasenia
    FROM 
        Usuario
    WHERE
	 (@IdRol IS NULL OR IdRol = @IdRol)
	 AND(@Nombre IS NULL OR Nombre LIKE '%' + @Nombre + '%')
     AND (@Apellido IS NULL OR Apellido LIKE '%' + @Apellido + '%')
	 AND (@Celular IS NULL OR Celular LIKE '%' + @Celular + '%')
	 AND (@Cuenta IS NULL OR Cuenta LIKE '%' + @Cuenta + '%')
	 AND (@Contrasenia IS NULL OR @Contrasenia LIKE '%' + @Contrasenia	+ '%')
END
GO
CREATE PROCEDURE VerificarUsuarioLogin
     @IdRol INT=NULL,
	 @Cuenta VARCHAR(50)=NULL,
	 @Contrasenia VARCHAR(20)=NULL
AS
BEGIN
   SELECT ISNULL( 
   (SELECT 1 FROM Usuario 
   WHERE IdRol=@IdRol AND 
   Cuenta=@Cuenta AND
   Contrasenia=@Contrasenia), 0)
AS UserExiste
END
GO
CREATE PROCEDURE GuardarCliente
    @IdRol INT,
    @IdTarjeta INT = NULL,
    @IdTipoDocumento INT,
    @Documento VARCHAR(50),
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @Celular VARCHAR(9)
AS
BEGIN
    INSERT INTO Cliente (IdRol, IdTarjeta, IdTipoDocumento, Documento, Nombre, Apellido, Celular)
    VALUES (@IdRol, @IdTarjeta, @IdTipoDocumento, @Documento, @Nombre, @Apellido, @Celular);
END
GO
CREATE PROCEDURE EliminarCliente
    @Id INT
AS
BEGIN
    DELETE FROM Cliente WHERE Id = @Id;
END
GO

-- Modificar cliente
CREATE PROCEDURE ModificarCliente
    @Id INT,
    @IdRol INT,
    @IdTarjeta INT = NULL,
    @IdTipoDocumento INT,
    @Documento VARCHAR(50),
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @Celular VARCHAR(9)
AS
BEGIN
    UPDATE Cliente
    SET IdRol = @IdRol,
        IdTarjeta = @IdTarjeta,
        IdTipoDocumento = @IdTipoDocumento,
        Documento = @Documento,
        Nombre = @Nombre,
        Apellido = @Apellido,
        Celular = @Celular
    WHERE Id = @Id;
END
GO
CREATE PROCEDURE MostrarCliente
    @IdRol INT = NULL,
    @IdTarjeta INT = NULL,
    @IdTipoDocumento INT = NULL,
    @Documento VARCHAR(50) = NULL,
    @Nombre VARCHAR(50) = NULL,
    @Apellido VARCHAR(50) = NULL,
    @Celular VARCHAR(9) = NULL
AS
BEGIN
    SELECT Id, IdRol, IdTarjeta, IdTipoDocumento, Documento, Nombre, Apellido, Celular
    FROM Cliente
    WHERE (@IdRol IS NULL OR IdRol = @IdRol)
      AND (@IdTarjeta IS NULL OR IdTarjeta = @IdTarjeta)
      AND (@IdTipoDocumento IS NULL OR IdTipoDocumento = @IdTipoDocumento)
      AND (@Documento IS NULL OR Documento LIKE '%' + @Documento + '%')
      AND (@Nombre IS NULL OR Nombre LIKE '%' + @Nombre + '%')
      AND (@Apellido IS NULL OR Apellido LIKE '%' + @Apellido + '%')
      AND (@Celular IS NULL OR Celular LIKE '%' + @Celular + '%');
END
GO
CREATE PROCEDURE ObtenerTarjetaPorNumero
    @Numerorfid VARCHAR(50)
AS
BEGIN
    SELECT Id, Numerorfid
    FROM Tarjeta
    WHERE Numerorfid = @Numerorfid;
END
GO
CREATE PROCEDURE MostrarAsistencia
    @FechaDesde DATE = NULL,
    @FechaHasta DATE = NULL
AS
BEGIN
    SELECT 
        Id,
        IdCliente,
        IdTarjeta,
        NombreCliente,
        ApellidoCliente,
        DocumentoCliente,
        FechaAsistencia,
        HoraAsistencia
    FROM RegistroAsistencia
    WHERE
        (@FechaDesde IS NULL OR FechaAsistencia >= @FechaDesde)
        AND (@FechaHasta IS NULL OR FechaAsistencia <= @FechaHasta)
    ORDER BY FechaAsistencia DESC, HoraAsistencia DESC;
END
GO
GO

-- Registrar asistencia (ya lo tienes, solo recordatorio)
CREATE PROCEDURE RegistrarAsistenciaPorTarjeta
    @IdTarjeta INT
AS
BEGIN
    DECLARE @IdCliente INT,
            @Nombre VARCHAR(50),
            @Apellido VARCHAR(50),
            @Documento VARCHAR(50),
            @HoraActual TIME = CAST(GETDATE() AS TIME);

    SELECT TOP 1
        @IdCliente = C.Id,
        @Nombre = C.Nombre,
        @Apellido = C.Apellido,
        @Documento = C.Documento
    FROM Cliente C
    WHERE C.IdTarjeta = @IdTarjeta;

    IF @IdCliente IS NOT NULL
    BEGIN
        INSERT INTO RegistroAsistencia 
        (IdCliente, IdTarjeta, NombreCliente, ApellidoCliente, DocumentoCliente, FechaAsistencia, HoraAsistencia)
        VALUES 
        (@IdCliente, @IdTarjeta, @Nombre, @Apellido, @Documento, CAST(GETDATE() AS DATE), @HoraActual);
    END
    ELSE
    BEGIN
        PRINT '❌ No se encontró un cliente con esa tarjeta';
    END
END
GO

---Buscar 
CREATE PROCEDURE BuscarUsuario
@Nombre VARCHAR(50)
AS
SELECT*FROM Usuario WHERE Nombre LIKE @Nombre + '%'
GO
CREATE PROCEDURE BuscarMembresia
@Nombre VARCHAR(30)
AS
SELECT*FROM Membresia WHERE Nombre LIKE @Nombre + '%'
GO
CREATE PROCEDURE BuscarInscripcion
    @IdCliente INT
AS
BEGIN
    SELECT * FROM Inscripcion
    WHERE @IdCliente = @IdCliente
END
GO
---VerificarLogin----
CREATE PROCEDURE ValidarUsuario
    @Cuenta VARCHAR(50),
    @Contrasenia VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IdRol
    FROM Usuario
    WHERE Cuenta = @Cuenta AND Contrasenia = @Contrasenia;
END
GO

EXEC GuardarRol Administrador
EXEC GuardarRol Empleado
EXEC GuardarRol Cliente
EXEC GuardarUsuario 1,William,Rosa,70294311,William791,791
EXEC GuardarEstado Activo
EXEC GuardarEstado Inactivo

