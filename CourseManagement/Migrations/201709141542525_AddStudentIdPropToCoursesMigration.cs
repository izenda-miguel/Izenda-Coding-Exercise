namespace CourseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStudentIdPropToCoursesMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CourseGrades", "Student_Id", "dbo.Students");
            DropIndex("dbo.CourseGrades", new[] { "Student_Id" });
            RenameColumn(table: "dbo.CourseGrades", name: "Student_Id", newName: "StudentId");
            AlterColumn("dbo.CourseGrades", "StudentId", c => c.Int(nullable: false));
            CreateIndex("dbo.CourseGrades", "StudentId");
            AddForeignKey("dbo.CourseGrades", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseGrades", "StudentId", "dbo.Students");
            DropIndex("dbo.CourseGrades", new[] { "StudentId" });
            AlterColumn("dbo.CourseGrades", "StudentId", c => c.Int());
            RenameColumn(table: "dbo.CourseGrades", name: "StudentId", newName: "Student_Id");
            CreateIndex("dbo.CourseGrades", "Student_Id");
            AddForeignKey("dbo.CourseGrades", "Student_Id", "dbo.Students", "Id");
        }
    }
}
