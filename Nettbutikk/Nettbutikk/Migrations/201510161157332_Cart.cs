namespace Nettbutikk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Cart_Id = c.String(nullable: false, maxLength: 128),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.Cart_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Cart_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.CartItems", "Cart_Id", "dbo.Carts");
            DropIndex("dbo.CartItems", new[] { "Product_Id" });
            DropIndex("dbo.CartItems", new[] { "Cart_Id" });
            DropTable("dbo.CartItems");
            DropTable("dbo.Carts");
        }
    }
}
