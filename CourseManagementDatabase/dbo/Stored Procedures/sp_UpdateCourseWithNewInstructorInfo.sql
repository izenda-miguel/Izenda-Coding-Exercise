CREATE PROCEDURE sp_UpdateCourseWithNewInstructorInfo
(
	@id int,
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

	UPDATE Courses
	SET
		CourseName = @courseName,
		CourseDescription = @courseDescription,
		InstructorId = @instructorId,
		StartDate = @startDate,
		EndDate = @endDate,
		CreditHours = @creditHours
	WHERE 
		Id = @id

END

