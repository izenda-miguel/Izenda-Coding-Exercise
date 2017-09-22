namespace CourseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserTypeColumnsMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Administrators", "UserType", c => c.String());
            AlterColumn("dbo.Credentials", "UserType", c => c.String());
            AlterColumn("dbo.Instructors", "UserType", c => c.String());
            AlterColumn("dbo.Students", "UserType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "UserType", c => c.Int(nullable: false));
            AlterColumn("dbo.Instructors", "UserType", c => c.Int(nullable: false));
            AlterColumn("dbo.Credentials", "UserType", c => c.Int(nullable: false));
            AlterColumn("dbo.Administrators", "UserType", c => c.Int(nullable: false));
        }
    }
}
