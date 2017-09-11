CREATE TABLE [dbo].[UserCredentials] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (255) NULL,
    [LastName]  NVARCHAR (255) NULL,
    [Username]  NVARCHAR (100) NOT NULL,
    [Password]  NVARCHAR (100) NOT NULL,
    [UserType]  NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

