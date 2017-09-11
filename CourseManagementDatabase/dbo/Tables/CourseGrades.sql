CREATE TABLE [dbo].[CourseGrades] (
    [Id]         INT          IDENTITY (1, 1) NOT NULL,
    [CourseId]   INT          NOT NULL,
    [StudentId]  INT          NOT NULL,
    [FinalGrade] NVARCHAR (2) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CourseGrades_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Students] ([Id])
);

