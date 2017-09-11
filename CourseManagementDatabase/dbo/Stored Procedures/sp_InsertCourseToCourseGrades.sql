CREATE PROCEDURE sp_InsertCourseToCourseGrades
(
	@courseId int,
	@studentId int
)
AS
BEGIN
    SET NOCOUNT ON;

	INSERT INTO CourseGrades
	(
		CourseId,
		StudentId
	)
	VALUES
	(
		@courseId,
		@studentId
	)

END

