using System;
using System.Collections.Generic;
using CourseManagement.Core;
using CourseManagement.Core.Account;
using CourseManagement.Enums;
using CourseManagement.Helpers;
using CourseManagement.Models;

namespace CourseManagementConsole
{
    /// <summary>
    /// The account's functions.
    /// </summary>
    public class AccountFunctions
    {
        private readonly AccountBase account;
        private readonly CourseManager courseManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountFunctions"/> class.
        /// </summary>
        /// <param name="account">The user's account class.</param>
        public AccountFunctions(AccountBase account)
        {
            this.account = account;
            this.courseManager = new CourseManager();
        }

        /// <summary>
        /// Maps the user's permissions to its function.
        /// </summary>
        /// <param name="permission">The permission to be mapped.</param>
        public void MapPermissionsToFunction(Permissions permission)
        {
            switch (permission)
            {
                case Permissions.ViewCurrentUserInformation:
                    ViewCurrentUserInformation();
                    break;
                case Permissions.CreateCourse:
                    CreateCourse();
                    break;
                case Permissions.ViewCourse:
                    ViewCourse();
                    break;
                case Permissions.UpdateCourse:
                    UpdateCourse();
                    break;
                case Permissions.DeleteCourse:
                    DeleteCourse();
                    break;
                case Permissions.RegisterForCourse:
                    RegisterForCourse();
                    break;
                case Permissions.SubmitCourseGrades:
                    SubmitFinalCourseGrades();
                    break;
                case Permissions.AssignCourseInstructors:
                    AssignCourseInstructors();
                    break;
            }
        }

        /// <summary>
        /// View the current user's information.
        /// </summary>
        private void ViewCurrentUserInformation()
        {
            Console.WriteLine(this.account.GetAndSetFullUserInformation().ToString());
        }

        /// <summary>
        /// Creates a new course.
        /// </summary>
        private void CreateCourse()
        {
            Console.WriteLine("Lets create a course! Please fill out the following information below: ");

            var course = new Course();
            Console.Write("Course Name: ");
            course.CourseName = Console.ReadLine().Trim();

            Console.Write("Course description: ");
            course.CourseDescription = Console.ReadLine();

            var creditHours = 0.0;
            do
            {
                Console.Write("Credit hours: ");
                double.TryParse(Console.ReadLine(), out creditHours);
            } while (creditHours == 0.0);
            course.CreditHours = creditHours;

            course.StartDate = ConsoleExtensions.WriteThenReadDateTime("Start Date");

            course.EndDate = ConsoleExtensions.WriteThenReadDateTime("End Date");

            Console.Write("Is the instructor a current instructor, and do you have the instructors information? ");
            var isInstructorInfoAvail = ConsoleExtensions.WriteThenReadForYesOrNo();

            int instructorId = 0;
            string instructorUsername = null;
            if (isInstructorInfoAvail == "yes")
            {
                do
                {
                    Console.WriteLine("Please fill out either the instructor's id or the instructor's username: ");

                    Console.Write("Instructor id: ");
                    int.TryParse(Console.ReadLine(), out instructorId);

                    Console.Write("Instructor username: ");
                    instructorUsername = Console.ReadLine();
                } while (instructorId == 0 && string.IsNullOrEmpty(instructorUsername));
            }

            course.InstructorId = instructorId;
            this.courseManager.CreateCourse(course, instructorUsername);

            Console.WriteLine();
            Console.WriteLine("The course has been created! See information below: ");
            Console.WriteLine(this.courseManager.LookUpSpecificCourse(null, course.Id).ToString());
        }

        /// <summary>
        /// Views the specified course.
        /// </summary>
        private void ViewCourse()
        {
            Console.WriteLine("Do you want to view all courses, or view a specific course? In order to view a specific course, you need either the course name or course id.");
            int courseOption;
            do
            {
                Console.WriteLine("Please enter one of the options below: ");
                Console.WriteLine();
                Console.WriteLine("\t 1 \t View all courses");
                Console.WriteLine("\t 2 \t View specific course");
                int.TryParse(Console.ReadLine(), out courseOption);
                Console.WriteLine();
            } while (courseOption != 1 && courseOption != 2);

            if (courseOption == 2)
            {
                var course = this.ReadAndGetSpecificCourse();
                Console.WriteLine("Please find the course information below: ");
                Console.WriteLine(course);
            }
            else
            {
                this.PrintListOfAllCourses(this.courseManager.GetAllCourses());
            }
        }

        /// <summary>
        /// Gets the specific course.
        /// </summary>
        /// <returns>Returns the specific course.</returns>
        private Course ReadAndGetSpecificCourse()
        {
            int courseId;
            string courseName = null;
            do
            {
                Console.WriteLine("Please fill out either the course name or the course id: ");

                Console.Write("Course name: ");
                courseName = Console.ReadLine();

                Console.Write("Course id: ");
                int.TryParse(Console.ReadLine(), out courseId);
                Console.WriteLine();
            } while (courseId == 0 && string.IsNullOrEmpty(courseName));

            return this.courseManager.LookUpSpecificCourse(courseName, courseId);
        }

        /// <summary>
        /// Prints all the courses.
        /// </summary>
        /// <param name="courses">List of courses.</param>
        private void PrintListOfAllCourses(List<Course> courses)
        {
            Console.WriteLine(string.Join(Environment.NewLine + Environment.NewLine, courses));
        }

        /// <summary>
        /// Updates a specific course.
        /// </summary>
        private void UpdateCourse()
        {
            Console.WriteLine("In order to update a course, you need the course name or the course id.");
            Course oldCourse;
            string isCorrectCourse;
            do
            {
                oldCourse = this.ReadAndGetSpecificCourse();
                Console.WriteLine("Please confirm that the course below is the one you want to update: ");
                Console.WriteLine(oldCourse);
                isCorrectCourse = ConsoleExtensions.WriteThenReadForYesOrNo();
            } while (isCorrectCourse.ToLower() != "yes");

            Course updatedCourse = null;
            while (updatedCourse == null)
            {
                Console.WriteLine("What property do you want to update from the course?");
                Console.WriteLine("Options: Name, Description, Start date, End date, Credit hours, or Instructor (enter the new instructors id).");
                var property = Console.ReadLine();

                Console.WriteLine("What do you want to update this field to? ");
                Console.Write($"{property.UppercaseFirst()}: ");
                var value = Console.ReadLine();
                Console.WriteLine();

                updatedCourse = CourseExtensions.UpdateCoursePropertyBasedOnStringAndValue(oldCourse, property, value);
                if (updatedCourse == null)
                {
                    Console.WriteLine("There was an error trying to update your course. Please make sure your information is correct and try again.");
                }
            }

            var succesfulUpdatedCourse = this.courseManager.UpdateExistingCourse(oldCourse, updatedCourse);
            Console.WriteLine("The course has been updated succesfully! See below for updated course: ");
            Console.WriteLine(succesfulUpdatedCourse);
        }

        /// <summary>
        /// Deletes a course.
        /// </summary>
        private void DeleteCourse()
        {
            Console.WriteLine("In order to delete a course, you need the course name or the course id.");
            Course course;
            string isCorrectCourse;
            do
            {
                course = this.ReadAndGetSpecificCourse();
                Console.WriteLine("Reminder that once this course is deleted, there is no way to get the information and students registered back.");
                Console.WriteLine("Please confirm the course below before deleting: ");
                Console.WriteLine(course);
                isCorrectCourse = ConsoleExtensions.WriteThenReadForYesOrNo();
            } while (isCorrectCourse.ToLower() != "yes");

            var successfulDeletion = this.courseManager.TryDeleteCourse(course);
            Console.WriteLine();
            var consoleMessage = successfulDeletion ? "The course was deleted succesfully!" : "There was an error deleting the course, please try again...";
            Console.WriteLine(consoleMessage);
        }

        /// <summary>
        /// Register for a course.
        /// </summary>
        private void RegisterForCourse()
        {
            Console.WriteLine("In order to register for a course, you need to have the course name or the course id.");
            var studentAccount = (StudentAccount)this.account;
            
            Course course;
            string isCorrectCourse;
            do
            {
                course = this.ReadAndGetSpecificCourse();
                Console.WriteLine("Is the following course correct: ");
                Console.WriteLine(course);
                isCorrectCourse = ConsoleExtensions.WriteThenReadForYesOrNo();
            } while (isCorrectCourse.ToLower() != "yes");

            studentAccount.RegisterForCourse(course);

            Console.WriteLine("You have succesfully registered for the course!");
        }

        /// <summary>
        /// Submit final course grades.
        /// </summary>
        private void SubmitFinalCourseGrades()
        {
            Console.WriteLine("In order to submit course grades, you must have the course id, and the student id.");
            CourseGrade courseGrade = new CourseGrade();
            string isCorrectCourse;
            do
            {
                courseGrade.Course = this.ReadAndGetSpecificCourse();
                Console.WriteLine("Please confirm the course below: ");
                Console.WriteLine(courseGrade.Course);
                isCorrectCourse = ConsoleExtensions.WriteThenReadForYesOrNo();
            } while (isCorrectCourse.ToLower() != "yes");

            Student student;
            do
            {
                Console.WriteLine("Please enter the student id and their final letter grade for the course: ");
                Console.Write("Student's id: ");
                var studentId = Convert.ToInt32(Console.ReadLine());
                LetterGrade finalGrade;
                Console.Write("Final Letter Grade: ");
                Enum.TryParse(Console.ReadLine().UppercaseFirst(), out finalGrade);
                courseGrade.FinalGrade = finalGrade;
                student = this.courseManager.SubmitStudentFinalCourseGrades(courseGrade, studentId);
                Console.WriteLine();
            } while (student == null);

            Console.WriteLine("Succesfully submitted the student's final grade!");
            Console.WriteLine(student);
        }

        /// <summary>
        /// Assign course instructors.
        /// </summary>
        private void AssignCourseInstructors()
        {
            Console.WriteLine("In order to assign a course instructor, you need the course name or course id, and the instructor id.");
            Course course;
            string isCorrectCourse;
            do
            {
                course = this.ReadAndGetSpecificCourse();
                Console.WriteLine("Please confirm the course below is the one you want to assign an instructor to: ");
                Console.WriteLine(course);
                isCorrectCourse = ConsoleExtensions.WriteThenReadForYesOrNo();
            } while (isCorrectCourse.ToLower() != "yes");

            Instructor instructor;
            do
            {
                Console.WriteLine("Please enter the instructor's id which you want to assign to this course.");
                Console.Write("Instructor's id: ");
                var instructorId = 0;
                int.TryParse(Console.ReadLine(), out instructorId);
                course.InstructorId = instructorId;
                instructor = this.courseManager.AssignCourseInstructor(course);
                Console.WriteLine();
            } while (instructor == null);

            Console.WriteLine($"You have succesfully assigned the following instructor to {course.CourseName}: ");
            Console.WriteLine(instructor);
        }
    }
}
