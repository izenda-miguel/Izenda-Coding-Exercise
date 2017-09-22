namespace CourseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGradeLevelToStudentsMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "GradeLevel", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "GradeLevel");
        }
    }
}
