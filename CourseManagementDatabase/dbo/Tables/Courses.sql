CREATE TABLE [dbo].[Courses] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [CourseName]        NVARCHAR (100) NULL,
    [CourseDescription] NVARCHAR (300) NULL,
    [InstructorId]      INT            NULL,
    [StartDate]         DATE           NULL,
    [EndDate]           DATE           NULL,
    [CreditHours]       DECIMAL (5, 2) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Courses_InstructorId] FOREIGN KEY ([InstructorId]) REFERENCES [dbo].[Instructors] ([Id])
);

