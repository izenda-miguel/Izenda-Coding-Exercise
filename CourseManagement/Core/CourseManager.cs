using System;
using System.Collections.Generic;
using CourseManagement.Models;
using CourseManagement.DataAccess;

namespace CourseManagement.Core
{
    /// <summary>
    /// The course manager.
    /// </summary>
    public class CourseManager
    {
        private readonly CourseDataManager courseDataManager;
        private readonly CourseGradesDataManager courseGradesDataManager;
        private readonly InstructorAccountDataManager instructorAccountDataManager;
        private readonly StudentAccountDataManager studentAccountDataManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CourseManager"/> class.
        /// </summary>
        public CourseManager()
        {
            this.courseDataManager = new CourseDataManager();
            this.courseGradesDataManager = new CourseGradesDataManager();
            this.studentAccountDataManager = new StudentAccountDataManager();
            this.instructorAccountDataManager = new InstructorAccountDataManager();
        }

        /// <summary>
        /// Creates a new course
        /// </summary>
        /// <param name="course">The course to be created.</param>
        /// <param name="instructorUsername">The instructor's username.</param>
        /// <param name="instructorId">The instructor's id.</param>
        public void CreateCourse(Course course, string instructorUsername, int instructorId)
        {
            course.Instructor = this.LookUpInstructor(instructorId, instructorUsername);

            if (course.Instructor != null)
            {
                this.courseDataManager.InsertNewCourseWithInstructor(course).Wait();
            }
            else
            {
                this.courseDataManager.InsertNewCourse(course).Wait();
            }
        }

        /// <summary>
        /// Looks up a specific instructor.
        /// </summary>
        /// <param name="id">The instructor's id.</param>
        /// <param name="username">The instructor's username.</param>
        /// <returns>Returns the instructor information or null if not found.</returns>
        private Instructor LookUpInstructor(int id = 0, string username = null)
        {
            Instructor instructor = null;

            if (id != 0)
            {
                instructor = this.instructorAccountDataManager.GetInstructorInfoBasedOnId(id).Result;
            }

            if (instructor == null && !string.IsNullOrEmpty(username))
            {
                instructor = this.instructorAccountDataManager.GetInstructorInfoBasedOnUsername(username).Result;
            }

            return instructor;
        }

        /// <summary>
        /// Looks up a specific student.
        /// </summary>
        /// <param name="id">The student's id.</param>
        /// <param name="username">The student's username.</param>
        /// <returns>Returns the student's information or null if not found.</returns>
        private Student LookUpStudent(int id = 0, string username = null)
        {
            Student student = null;

            if (id != 0)
            {
                student = this.studentAccountDataManager.GetStudentAccountInfoBasedOnId(id).Result;
            }

            if (student == null && !string.IsNullOrEmpty(username))
            {
                student = this.studentAccountDataManager.GetStudentAccountInfo(username).Result;
            }

            return student;
        }

        /// <summary>
        /// Looks up a specific course.
        /// </summary>
        /// <param name="courseName">The course name.</param>
        /// <param name="courseId">The course id.</param>
        /// <returns>Returns the course information or null if not found.</returns>
        public Course LookUpSpecificCourse(string courseName = null, int courseId = 0)
        {
            Course course = null;

            if (courseId != 0)
            {
                course = this.courseDataManager.GetSpecificCourseInfoBasedOnId(courseId).Result;
            }

            if (course == null && !string.IsNullOrEmpty(courseName))
            {
                course = this.courseDataManager.GetSpecificCourseInfoBasedOnCourseName(courseName).Result;
            }

            return course;
        }

        /// <summary>
        /// Gets all the courses.
        /// </summary>
        /// <returns>Returns all the courses.</returns>
        public List<Course> GetAllCourses()
        {
            return new List<Course>();
        }

        /// <summary>
        /// Updates an existing course.
        /// </summary>
        /// <param name="oldCourse">The old course to be updated.</param>
        /// <param name="updatedCourse">The new course information.</param>
        /// <returns>Returns the new course information.</returns>
        public Course UpdateExistingCourse(Course oldCourse, Course updatedCourse)
        {
            if (oldCourse?.Instructor?.Id != updatedCourse.Instructor.Id)
            {
                var assignedInstructor = this.AssignCourseInstructor(updatedCourse);

                if (assignedInstructor == null)
                {
                    this.courseDataManager.UpdateExistingCourse(updatedCourse).Wait();
                }
            }
            else
            {
                this.courseDataManager.UpdateExistingCourse(updatedCourse).Wait();
            }

            return this.LookUpSpecificCourse(null, updatedCourse.Id);
        }

        /// <summary>
        /// Assigns a course instructor.
        /// </summary>
        /// <param name="course">The course to assign.</param>
        /// <returns>Returns the instructor information.</returns>
        public Instructor AssignCourseInstructor(Course course)
        {
            course.Instructor = this.LookUpInstructor(course.Instructor.Id);

            if (course.Instructor != null)
            {
                this.courseDataManager.UpdateCourseWithNewInstructorInfo(course).Wait();
            }
            
            return course.Instructor;
        }

        /// <summary>
        /// Submits student's final course grades.
        /// </summary>
        /// <param name="courseGrade">The course grade.</param>
        /// <param name="studentId">The student's id.</param>
        /// <returns>Returns the student's information.</returns>
        public Student SubmitStudentFinalCourseGrades(CourseGrade courseGrade, int studentId)
        {
            var student = this.LookUpStudent(studentId);

            if (student != null)
            {
                this.courseGradesDataManager.UpdateFinalGrade(courseGrade, studentId).Wait();
            }

            student.Courses = this.courseGradesDataManager.GetStudentsCourseGrades(student.Id).Result;
            return student;
        }

        /// <summary>
        /// Deletes a course.
        /// </summary>
        /// <param name="course">The course to be deleted.</param>
        /// <returns>Returns whether or not the course was deleted succesfully.</returns>
        public bool TryDeleteCourse(Course course)
        {
            try
            {
                this.courseDataManager.DeleteExistingCourse(course).Wait();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
