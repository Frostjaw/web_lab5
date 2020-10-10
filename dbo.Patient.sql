CREATE TABLE [dbo].[Patient] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (50) NULL,
    [Doctor_id] INT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Patient_ToDoctor] FOREIGN KEY ([Doctor_id]) REFERENCES [dbo].[Doctor] ([Id])
);

