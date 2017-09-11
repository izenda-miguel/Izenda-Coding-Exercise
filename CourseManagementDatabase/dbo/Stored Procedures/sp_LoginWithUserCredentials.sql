CREATE PROCEDURE sp_LoginWithUserCredentials
(
	@username NVARCHAR(100),
	@password NVARCHAR(100),
	@userType NVARCHAR(100)
)
AS
BEGIN
     SET NOCOUNT ON;

	 SELECT Username, FirstName, LastName
	 FROM 
	 UserCredentials
	 WHERE 
	 Username = @username
	 AND
	 UserType = @userType
	 AND
	 Password = @password
	
END

