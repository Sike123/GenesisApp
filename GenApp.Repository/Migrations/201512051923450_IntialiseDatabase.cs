namespace GenApp.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntialiseDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Asset",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Isbn = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Asset", "User_Id", "dbo.User");
            DropIndex("dbo.Asset", new[] { "User_Id" });
            DropTable("dbo.User");
            DropTable("dbo.Asset");
        }
    }
}
