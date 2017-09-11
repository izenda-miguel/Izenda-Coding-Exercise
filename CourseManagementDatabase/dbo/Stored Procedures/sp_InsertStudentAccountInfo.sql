CREATE PROCEDURE sp_InsertStudentAccountInfo
(
	@firstName NVARCHAR(255),
	@lastName NVARCHAR(255),	
	@username NVARCHAR(100),
	@password NVARCHAR(100),
	@userType NVARCHAR(100),
	@gpa decimal(3, 2),
	@creditHours decimal(6, 2),
	@level nvarchar(100)
)
AS
BEGIN
    SET NOCOUNT ON;

	INSERT INTO Students
	(
		FirstName,
		LastName,
		GPA, 
		Username, 
		Password,
		CreditHours,
		Level,
		UserType
	)
	VALUES
	(
		@firstName,
		@lastName,
		@gpa,
		@username,
		@password,
		@creditHours,
		@level,
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

