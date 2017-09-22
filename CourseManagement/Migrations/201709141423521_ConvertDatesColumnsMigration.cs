namespace CourseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConvertDatesColumnsMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Administrators", "HireDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Courses", "StartDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Courses", "EndDate", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Instructors", "HireDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Instructors", "HireDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Courses", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Courses", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Administrators", "HireDate", c => c.DateTime(nullable: false));
        }
    }
}
