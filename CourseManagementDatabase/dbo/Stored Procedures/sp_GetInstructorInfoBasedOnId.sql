CREATE PROCEDURE sp_GetInstructorInfoBasedOnId
(
	@id int
)
AS
BEGIN
     SET NOCOUNT ON;

	 SELECT Id, FirstName, LastName, Username, HireDate
	 FROM 
	 Instructors
	 WHERE 
	 Id = @id
	
END

