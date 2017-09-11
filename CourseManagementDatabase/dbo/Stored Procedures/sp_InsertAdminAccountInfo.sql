CREATE PROCEDURE sp_InsertAdminAccountInfo
(
	@firstName NVARCHAR(255),
	@lastName NVARCHAR(255),	
	@username NVARCHAR(100),
	@password NVARCHAR(100),
	@userType NVARCHAR(100),
	@hireDate date
)
AS
BEGIN
    SET NOCOUNT ON;

	INSERT INTO Administrators
	(
		FirstName,
		LastName,
		Username, 
		Password,
		HireDate,
		UserType
	)
	VALUES
	(
		@firstName,
		@lastName,
		@username,
		@password,
		@hireDate,
		@userType
	)

	INSERT INTO UserCredentials
	(
		FirstName,
		LastName, 
		Username, 
		Password,
		UserType
	)
	VALUES
	(
		@firstName,
		@lastName,
		@username,
		@password,
		@userType
	)
END

