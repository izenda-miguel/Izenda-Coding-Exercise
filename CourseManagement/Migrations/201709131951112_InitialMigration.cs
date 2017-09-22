namespace CourseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HireDate = c.DateTime(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserType = c.Int(nullable: false),
                        Credentials_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Credentials", t => t.Credentials_Id)
                .Index(t => t.Credentials_Id);
            
            CreateTable(
                "dbo.Credentials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                        CourseDescription = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CreditHours = c.Double(nullable: false),
                        Instructor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Instructors", t => t.Instructor_Id)
                .Index(t => t.Instructor_Id);
            
            CreateTable(
                "dbo.Instructors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HireDate = c.DateTime(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserType = c.Int(nullable: false),
                        Credentials_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Credentials", t => t.Credentials_Id)
                .Index(t => t.Credentials_Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GPA = c.Double(nullable: false),
                        CreditHours = c.Double(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserType = c.Int(nullable: false),
                        Credentials_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Credentials", t => t.Credentials_Id)
                .Index(t => t.Credentials_Id);
            
            CreateTable(
                "dbo.CourseGrades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FinalGrade = c.Int(nullable: false),
                        Course_Id = c.Int(),
                        Student_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.Course_Id)
                .Index(t => t.Student_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "Credentials_Id", "dbo.Credentials");
            DropForeignKey("dbo.CourseGrades", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.CourseGrades", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Courses", "Instructor_Id", "dbo.Instructors");
            DropForeignKey("dbo.Instructors", "Credentials_Id", "dbo.Credentials");
            DropForeignKey("dbo.Administrators", "Credentials_Id", "dbo.Credentials");
            DropIndex("dbo.CourseGrades", new[] { "Student_Id" });
            DropIndex("dbo.CourseGrades", new[] { "Course_Id" });
            DropIndex("dbo.Students", new[] { "Credentials_Id" });
            DropIndex("dbo.Instructors", new[] { "Credentials_Id" });
            DropIndex("dbo.Courses", new[] { "Instructor_Id" });
            DropIndex("dbo.Administrators", new[] { "Credentials_Id" });
            DropTable("dbo.CourseGrades");
            DropTable("dbo.Students");
            DropTable("dbo.Instructors");
            DropTable("dbo.Courses");
            DropTable("dbo.Credentials");
            DropTable("dbo.Administrators");
        }
    }
}
