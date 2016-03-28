namespace GenApp.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFewMorecolumnsforBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Asset", "Edition", c => c.String());
            AddColumn("dbo.Asset", "Publisher", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Asset", "Publisher");
            DropColumn("dbo.Asset", "Edition");
        }
    }
}
