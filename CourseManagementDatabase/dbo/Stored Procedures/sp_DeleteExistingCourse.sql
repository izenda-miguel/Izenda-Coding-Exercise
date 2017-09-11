CREATE PROCEDURE sp_DeleteExistingCourse
(
	@id int
)
AS
BEGIN
    SET NOCOUNT ON;

	DELETE FROM Courses
	WHERE
		Id = @id

END

