CREATE PROCEDURE sp_UpdateFinalGrade
(
	@studentId int,
	@courseId int,
	@finalGrade NVARCHAR(2)
)
AS
BEGIN
    SET NOCOUNT ON;

	UPDATE CourseGrades
	SET
		FinalGrade = @finalGrade
	WHERE 
		StudentId = @studentId
		AND
		CourseId = @courseId

END
