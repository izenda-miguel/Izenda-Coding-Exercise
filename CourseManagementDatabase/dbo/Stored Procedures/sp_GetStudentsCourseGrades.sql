CREATE PROCEDURE sp_GetStudentsCourseGrades
(
	@studentId INT
)
AS
BEGIN
     SET NOCOUNT ON;

	 SELECT 
	 	CG.CourseId, C.CourseName, C.CourseDescription, C.StartDate, C.EndDate, C.CreditHours, 
		I.FirstName as InstructorFirstName, I.LastName as InstructorLastName, CG.Id as CourseGradeId, CG.FinalGrade
	 FROM 
		CourseGrades CG
		INNER JOIN Courses C ON CG.CourseId = C.Id
		INNER JOIN Instructors I ON C.InstructorId = I.Id
	 WHERE 
		StudentId = @studentId
	
END

