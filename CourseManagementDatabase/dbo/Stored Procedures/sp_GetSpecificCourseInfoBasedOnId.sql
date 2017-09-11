CREATE PROCEDURE [dbo].[sp_GetSpecificCourseInfoBasedOnId]
(
	@id int
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
		C.Id = @id
	
END

