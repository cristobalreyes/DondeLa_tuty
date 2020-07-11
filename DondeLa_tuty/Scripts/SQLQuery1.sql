CREATE TABLE [dbo].[Categories]
(
	[CategoryId] INT NOT NULL PRIMARY KEY, 
    [Nombre] NVARCHAR(50) NULL, 
    [Descripcion] NVARCHAR(MAX) NULL
)

CREATE TABLE [dbo].[Producers]
(
	[ProducerId] INT NOT NULL PRIMARY KEY, 
    [Nombre] NVARCHAR(50) NULL
)

CREATE TABLE [dbo].[Items]
(
	[ItemId] INT NOT NULL PRIMARY KEY, 
    [CategoryId] INT NULL, 
    [ProducerId] INT NULL, 
    [Titulo] VARCHAR(50) NULL, 
    [Precio] INT NULL, 
    [ItemArtUrl] IMAGE NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId),
    FOREIGN KEY (ProducerId) REFERENCES Producers(ProducerId)
)