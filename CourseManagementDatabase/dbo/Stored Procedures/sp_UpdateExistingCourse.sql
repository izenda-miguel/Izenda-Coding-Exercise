CREATE PROCEDURE sp_UpdateExistingCourse
(
	@id int,
	@courseName NVARCHAR(100),
	@courseDescription NVARCHAR(300),	
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
		StartDate = @startDate,
		EndDate = @endDate,
		CreditHours = @creditHours
	WHERE 
		Id = @id

END

