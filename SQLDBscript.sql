/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2014 (12.0.2269)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [StudentiDB]
GO
/****** Object:  Table [dbo].[Kolegij]    Script Date: 16.10.2018. 21:21:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kolegij](
	[KolegijId] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [nvarchar](50) NOT NULL,
	[Trajanje] [int] NOT NULL,
 CONSTRAINT [PK_Kolegij] PRIMARY KEY CLUSTERED 
(
	[KolegijId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Popis]    Script Date: 16.10.2018. 21:21:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Popis](
	[PopisId] [int] IDENTITY(1,1) NOT NULL,
	[KolegijId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
 CONSTRAINT [PK_Popis] PRIMARY KEY CLUSTERED 
(
	[PopisId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Studenti]    Script Date: 16.10.2018. 21:21:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Studenti](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[Ime] [nvarchar](50) NOT NULL,
	[Prezime] [nvarchar](50) NOT NULL,
	[OIB] [nvarchar](11) NOT NULL,
	[DatumRodenja] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Studenti] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Popis]  WITH CHECK ADD  CONSTRAINT [FK_Popis_Kolegij] FOREIGN KEY([KolegijId])
REFERENCES [dbo].[Kolegij] ([KolegijId])
GO
ALTER TABLE [dbo].[Popis] CHECK CONSTRAINT [FK_Popis_Kolegij]
GO
ALTER TABLE [dbo].[Popis]  WITH CHECK ADD  CONSTRAINT [FK_Popis_Studenti] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Studenti] ([StudentId])
GO
ALTER TABLE [dbo].[Popis] CHECK CONSTRAINT [FK_Popis_Studenti]
GO
