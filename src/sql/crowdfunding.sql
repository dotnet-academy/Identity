SET XACT_ABORT ON;  

CREATE DATABASE [CrowdFunding]
GO

USE [CrowdFunding]
GO

BEGIN TRANSACTION;

CREATE TABLE [dbo].[Category](
	[CategoryId] [bigint],
	[Name] [nvarchar](50) NOT NULL,

	CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ( [CategoryId] ASC )
) 
GO

CREATE TABLE [dbo].[Person](
	[PersonId] [bigint] IDENTITY(1,1) NOT NULL,
	[Firstname] [nvarchar](50) NOT NULL,
	[Lastname] [nvarchar](50) NOT NULL,
	[ProfileUrl] [nvarchar](255) NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,

	CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ( [PersonId] ASC )
)
GO

CREATE TABLE [dbo].[Project](
	[ProjectId] [bigint] IDENTITY(1,1) NOT NULL,
	[PersonID] [bigint] NOT NULL,
	[CategoryID] [bigint] NOT NULL,
	[PictureUrl] [nvarchar](255) NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Deadline] [datetime2](7) NOT NULL,
	[Goal] [decimal](18, 2) NOT NULL,

	CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED ( [ProjectId] ASC )
)
GO

ALTER TABLE [dbo].[Project] ADD CONSTRAINT [FK_Project_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryId])
GO

ALTER TABLE [dbo].[Project] ADD CONSTRAINT [FK_Project_Person] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonId])
GO

INSERT INTO [dbo].[Category] VALUES (1, 'Games');
INSERT INTO [dbo].[Category] VALUES (2, 'Food & Craft');
INSERT INTO [dbo].[Category] VALUES (3, 'Film');
INSERT INTO [dbo].[Category] VALUES (4, 'Music');
INSERT INTO [dbo].[Category] VALUES (5, 'Design & Tech');
INSERT INTO [dbo].[Category] VALUES (6, 'Publishing');
INSERT INTO [dbo].[Category] VALUES (7, 'Arts');
INSERT INTO [dbo].[Category] VALUES (8, 'Comics & Illustration');

COMMIT TRANSACTION;