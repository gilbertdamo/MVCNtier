﻿CREATE TABLE [dbo].[Pet]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Type] NVARCHAR(MAX) NULL
)
