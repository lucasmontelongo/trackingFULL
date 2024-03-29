USE [master]
GO
/****** Object:  Database [trackingFULL]    Script Date: 6/12/2019 14:32:44 ******/
CREATE DATABASE [trackingFULL]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'trackingFULL', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\trackingFULL.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'trackingFULL_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\trackingFULL_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [trackingFULL] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [trackingFULL].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [trackingFULL] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [trackingFULL] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [trackingFULL] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [trackingFULL] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [trackingFULL] SET ARITHABORT OFF 
GO
ALTER DATABASE [trackingFULL] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [trackingFULL] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [trackingFULL] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [trackingFULL] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [trackingFULL] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [trackingFULL] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [trackingFULL] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [trackingFULL] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [trackingFULL] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [trackingFULL] SET  DISABLE_BROKER 
GO
ALTER DATABASE [trackingFULL] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [trackingFULL] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [trackingFULL] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [trackingFULL] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [trackingFULL] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [trackingFULL] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [trackingFULL] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [trackingFULL] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [trackingFULL] SET  MULTI_USER 
GO
ALTER DATABASE [trackingFULL] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [trackingFULL] SET DB_CHAINING OFF 
GO
ALTER DATABASE [trackingFULL] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [trackingFULL] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [trackingFULL] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [trackingFULL] SET QUERY_STORE = OFF
GO
USE [trackingFULL]
GO
/****** Object:  Table [dbo].[Agencia]    Script Date: 6/12/2019 14:32:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agencia](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](250) NULL,
	[ubicacion] [varchar](200) NULL,
	[envioDomicilio] [bit] NULL,
	[idEmpresa] [int] NULL,
	[borrado] [bit] NULL,
 CONSTRAINT [PK_Agencia] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 6/12/2019 14:32:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [varchar](50) NULL,
	[nombreCompleto] [varchar](250) NULL,
	[telefono] [varchar](250) NULL,
	[numeroDocumento] [varchar](250) NULL,
	[tipoDocumento] [varchar](50) NULL,
	[borrado] [bit] NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Domicilio]    Script Date: 6/12/2019 14:32:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Domicilio](
	[idPaquete] [int] NOT NULL,
	[envio] [bit] NULL,
 CONSTRAINT [PK_Domicilio] PRIMARY KEY CLUSTERED 
(
	[idPaquete] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empresa]    Script Date: 6/12/2019 14:32:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empresa](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[borrado] [bit] NULL,
 CONSTRAINT [PK_Empresa] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Paquete]    Script Date: 6/12/2019 14:32:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paquete](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigoConfirmacion] [varchar](10) NULL,
	[codigo] [varchar](max) NULL,
	[IdTrayecto] [int] NULL,
	[idRemitente] [int] NULL,
	[idDestinatario] [int] NULL,
	[fechaIngreso] [datetime] NULL,
	[fechaEntrega] [datetime] NULL,
	[borrado] [bit] NULL,
 CONSTRAINT [PK_Paquete] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaquetePuntoControl]    Script Date: 6/12/2019 14:32:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaquetePuntoControl](
	[idPaquete] [int] NULL,
	[idPuntoControl] [int] NULL,
	[fechaLlegada] [datetime] NULL,
	[idEmpleado] [int] NULL,
	[borrado] [bit] NULL,
	[retraso] [int] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_PaquetePuntoControl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PuntoControl]    Script Date: 6/12/2019 14:32:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PuntoControl](
	[idTrayecto] [int] NULL,
	[idAgencia] [int] NULL,
	[orden] [int] NULL,
	[tiempo] [int] NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](250) NULL,
	[borrado] [bit] NULL,
 CONSTRAINT [PK_PuntoControl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trayecto]    Script Date: 6/12/2019 14:32:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trayecto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[version] [int] NULL,
	[borrado] [bit] NULL,
 CONSTRAINT [PK_Trayecto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 6/12/2019 14:32:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [varchar](250) NULL,
	[password] [varchar](250) NULL,
	[rol] [varchar](50) NULL,
	[borrado] [bit] NULL,
	[emailValido] [bit] NULL,
	[codigoConfirmacion] [varchar](250) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_Usuario_Email] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Agencia] ADD  CONSTRAINT [DF_Agencia_borrado]  DEFAULT ((0)) FOR [borrado]
GO
ALTER TABLE [dbo].[Cliente] ADD  CONSTRAINT [DF_Cliente_borrado]  DEFAULT ((0)) FOR [borrado]
GO
ALTER TABLE [dbo].[Domicilio] ADD  CONSTRAINT [DF_Domicilio_envio]  DEFAULT ((0)) FOR [envio]
GO
ALTER TABLE [dbo].[Empresa] ADD  CONSTRAINT [DF_Empresa_borrado]  DEFAULT ((0)) FOR [borrado]
GO
ALTER TABLE [dbo].[Paquete] ADD  CONSTRAINT [DF_Paquete_fechaIngreso]  DEFAULT (getdate()) FOR [fechaIngreso]
GO
ALTER TABLE [dbo].[Paquete] ADD  CONSTRAINT [DF_Paquete_borrado]  DEFAULT ((0)) FOR [borrado]
GO
ALTER TABLE [dbo].[PaquetePuntoControl] ADD  CONSTRAINT [DF_PaquetePuntoControl_borrado]  DEFAULT ((0)) FOR [borrado]
GO
ALTER TABLE [dbo].[PuntoControl] ADD  CONSTRAINT [DF_PuntoControl_borrado]  DEFAULT ((0)) FOR [borrado]
GO
ALTER TABLE [dbo].[Trayecto] ADD  CONSTRAINT [DF_Trayecto_borrado]  DEFAULT ((0)) FOR [borrado]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_rol]  DEFAULT ('Cliente') FOR [rol]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_borrado]  DEFAULT ((0)) FOR [borrado]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_emailValido]  DEFAULT ((0)) FOR [emailValido]
GO
ALTER TABLE [dbo].[Agencia]  WITH CHECK ADD  CONSTRAINT [FK_Agencia_Empresa] FOREIGN KEY([idEmpresa])
REFERENCES [dbo].[Empresa] ([id])
GO
ALTER TABLE [dbo].[Agencia] CHECK CONSTRAINT [FK_Agencia_Empresa]
GO
ALTER TABLE [dbo].[Paquete]  WITH CHECK ADD  CONSTRAINT [FK_Paquete_Cliente] FOREIGN KEY([idRemitente])
REFERENCES [dbo].[Cliente] ([id])
GO
ALTER TABLE [dbo].[Paquete] CHECK CONSTRAINT [FK_Paquete_Cliente]
GO
ALTER TABLE [dbo].[Paquete]  WITH CHECK ADD  CONSTRAINT [FK_Paquete_Cliente1] FOREIGN KEY([idDestinatario])
REFERENCES [dbo].[Cliente] ([id])
GO
ALTER TABLE [dbo].[Paquete] CHECK CONSTRAINT [FK_Paquete_Cliente1]
GO
ALTER TABLE [dbo].[Paquete]  WITH CHECK ADD  CONSTRAINT [FK_Paquete_Trayecto] FOREIGN KEY([IdTrayecto])
REFERENCES [dbo].[Trayecto] ([id])
GO
ALTER TABLE [dbo].[Paquete] CHECK CONSTRAINT [FK_Paquete_Trayecto]
GO
ALTER TABLE [dbo].[PaquetePuntoControl]  WITH CHECK ADD  CONSTRAINT [FK_PaquetePuntoControl_Paquete] FOREIGN KEY([idPaquete])
REFERENCES [dbo].[Paquete] ([id])
GO
ALTER TABLE [dbo].[PaquetePuntoControl] CHECK CONSTRAINT [FK_PaquetePuntoControl_Paquete]
GO
ALTER TABLE [dbo].[PaquetePuntoControl]  WITH CHECK ADD  CONSTRAINT [FK_PaquetePuntoControl_PuntoControl2] FOREIGN KEY([idPuntoControl])
REFERENCES [dbo].[PuntoControl] ([id])
GO
ALTER TABLE [dbo].[PaquetePuntoControl] CHECK CONSTRAINT [FK_PaquetePuntoControl_PuntoControl2]
GO
ALTER TABLE [dbo].[PaquetePuntoControl]  WITH CHECK ADD  CONSTRAINT [FK_PaquetePuntoControl_Usuario] FOREIGN KEY([idEmpleado])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[PaquetePuntoControl] CHECK CONSTRAINT [FK_PaquetePuntoControl_Usuario]
GO
ALTER TABLE [dbo].[PuntoControl]  WITH CHECK ADD  CONSTRAINT [FK_PuntoControl_Agencia] FOREIGN KEY([idAgencia])
REFERENCES [dbo].[Agencia] ([id])
GO
ALTER TABLE [dbo].[PuntoControl] CHECK CONSTRAINT [FK_PuntoControl_Agencia]
GO
ALTER TABLE [dbo].[PuntoControl]  WITH CHECK ADD  CONSTRAINT [FK_PuntoControl_Trayecto] FOREIGN KEY([idTrayecto])
REFERENCES [dbo].[Trayecto] ([id])
GO
ALTER TABLE [dbo].[PuntoControl] CHECK CONSTRAINT [FK_PuntoControl_Trayecto]
GO
USE [master]
GO
ALTER DATABASE [trackingFULL] SET  READ_WRITE 
GO
