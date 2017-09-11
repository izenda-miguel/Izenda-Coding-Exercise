CREATE PROCEDURE sp_GetStudentAccountInfo
(
	@username NVARCHAR(100)
)
AS
BEGIN
     SET NOCOUNT ON;

	 SELECT Id, FirstName, LastName, GPA, Username, CreditHours
	 FROM 
	 Students
	 WHERE 
	 Username = @username
	
END

