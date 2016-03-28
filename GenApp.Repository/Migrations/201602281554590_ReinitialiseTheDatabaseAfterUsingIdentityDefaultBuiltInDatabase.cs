namespace GenApp.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReinitialiseTheDatabaseAfterUsingIdentityDefaultBuiltInDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Email", c => c.String());
            AddColumn("dbo.User", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "Password");
            DropColumn("dbo.User", "Email");
        }
    }
}
