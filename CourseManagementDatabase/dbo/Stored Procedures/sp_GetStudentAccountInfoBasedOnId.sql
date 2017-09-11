CREATE PROCEDURE sp_GetStudentAccountInfoBasedOnId
(
	@id int
)
AS
BEGIN
     SET NOCOUNT ON;

	 SELECT 
		Id, FirstName, LastName, GPA, Username, CreditHours
	 FROM 
		Students
	 WHERE 
		Id = @id
	
END

