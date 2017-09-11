CREATE PROCEDURE sp_GetAdminAccountInfo
(
	@username NVARCHAR(100)
)
AS
BEGIN
     SET NOCOUNT ON;

	 SELECT Id, FirstName, LastName, Username, HireDate
	 FROM 
	 Administrators
	 WHERE 
	 Username = @username
	
END

