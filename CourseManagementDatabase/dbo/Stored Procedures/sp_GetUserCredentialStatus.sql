CREATE PROCEDURE sp_GetUserCredentialStatus
(
	@username NVARCHAR(100)
)
AS
BEGIN
     SET NOCOUNT ON;

	 SELECT Username, FirstName, LastName
	 FROM 
	 UserCredentials
	 WHERE 
	 Username = @username
	
END

