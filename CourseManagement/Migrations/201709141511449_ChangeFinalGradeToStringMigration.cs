namespace CourseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFinalGradeToStringMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourseGrades", "FinalGrade", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourseGrades", "FinalGrade", c => c.Int(nullable: false));
        }
    }
}
