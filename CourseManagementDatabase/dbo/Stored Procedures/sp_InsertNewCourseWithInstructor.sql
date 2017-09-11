CREATE PROCEDURE sp_InsertNewCourseWithInstructor
(
	@courseName NVARCHAR(100),
	@courseDescription NVARCHAR(300),	
	@instructorId int,
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
		InstructorId,
		StartDate,
		EndDate,
		CreditHours
	)
	VALUES
	(
		@courseName,
		@courseDescription,
		@instructorId,
		@startDate,
		@endDate,
		@creditHours
	)

END

