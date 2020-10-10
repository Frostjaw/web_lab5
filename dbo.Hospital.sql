CREATE TABLE [dbo].[Hospital] (
    [Id]      INT          IDENTITY (1, 1) NOT NULL,
    [Name]    VARCHAR (50) NULL,
    [Address] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

