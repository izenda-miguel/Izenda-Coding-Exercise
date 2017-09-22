namespace CourseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeInstructorIdNullableMigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Courses", name: "Instructor_Id", newName: "InstructorId");
            RenameIndex(table: "dbo.Courses", name: "IX_Instructor_Id", newName: "IX_InstructorId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Courses", name: "IX_InstructorId", newName: "IX_Instructor_Id");
            RenameColumn(table: "dbo.Courses", name: "InstructorId", newName: "Instructor_Id");
        }
    }
}
