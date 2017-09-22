namespace CourseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeUsernameNotNullableMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Credentials", "Username", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Credentials", "Username", c => c.String());
        }
    }
}
