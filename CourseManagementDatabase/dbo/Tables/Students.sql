CREATE TABLE [dbo].[Students] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (255) NULL,
    [LastName]    NVARCHAR (255) NULL,
    [GPA]         DECIMAL (3, 2) NULL,
    [Username]    NVARCHAR (100) NOT NULL,
    [Password]    NVARCHAR (100) NOT NULL,
    [CreditHours] DECIMAL (6, 2) NULL,
    [Level]       NVARCHAR (100) NULL,
    [UserType]    NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

