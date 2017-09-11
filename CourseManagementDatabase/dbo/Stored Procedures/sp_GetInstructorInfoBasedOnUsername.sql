CREATE PROCEDURE sp_GetInstructorInfoBasedOnUsername
(
	@username NVARCHAR(100)
)
AS
BEGIN
     SET NOCOUNT ON;

	 SELECT Id, FirstName, LastName, Username, HireDate
	 FROM 
	 Instructors
	 WHERE 
	 Username = @username
	
END

