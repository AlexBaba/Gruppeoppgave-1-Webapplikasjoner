namespace Nettbutikk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserCredentials : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Carts", newName: "ShoppingCarts");
            CreateTable(
                "dbo.Credentials",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Password = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Credentials", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Credentials", new[] { "UserId" });
            DropTable("dbo.Credentials");
            RenameTable(name: "dbo.ShoppingCarts", newName: "Carts");
        }
    }
}
