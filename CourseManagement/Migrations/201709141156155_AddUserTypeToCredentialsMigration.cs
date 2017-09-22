namespace CourseManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserTypeToCredentialsMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Credentials", "UserType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Credentials", "UserType");
        }
    }
}
