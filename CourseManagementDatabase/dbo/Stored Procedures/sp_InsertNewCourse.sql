CREATE PROCEDURE sp_InsertNewCourse
(
	@courseName NVARCHAR(100),
	@courseDescription NVARCHAR(300),	
	@startDate date,
	@endDate date,
	@creditHours decimal(5, 2)
)
AS
BEGIN
    SET NOCOUNT ON;

	INSERT INTO Courses
	(
		CourseName,
		CourseDescription,
		StartDate,
		EndDate,
		CreditHours
	)
	VALUES
	(
		@courseName,
		@courseDescription,
		@startDate,
		@endDate,
		@creditHours
	)

END

