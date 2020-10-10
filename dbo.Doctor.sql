CREATE TABLE [dbo].[Doctor] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50) NULL,
    [Speciality]  VARCHAR (50) NULL,
    [Hospital_id] INT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Doctor_ToHospital] FOREIGN KEY ([Hospital_id]) REFERENCES [dbo].[Hospital] ([Id])
);

