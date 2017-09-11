CREATE PROCEDURE [dbo].[sp_GetSpecificCourseInfoBasedOnCourseName]
(
	@courseName NVARCHAR(100)
)
AS
BEGIN
     SET NOCOUNT ON;

	 SELECT 
		C.Id, C.CourseName, C.CourseDescription, C.StartDate, C.EndDate, C.CreditHours, I.Id as InstructorId, I.FirstName as InstructorFirstName, I.LastName as InstructorLastName
	 FROM 
		Courses C 
		LEFT JOIN Instructors I ON C.InstructorId = I.Id
	 WHERE 
		C.CourseName = @courseName
	
END

