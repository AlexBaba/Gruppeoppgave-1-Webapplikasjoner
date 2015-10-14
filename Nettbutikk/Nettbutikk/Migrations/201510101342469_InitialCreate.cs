namespace Nettbutikk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Stock = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        Imagebytes = c.Binary(),
                        ProductID_ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.ImageID)
                .ForeignKey("dbo.Products", t => t.ProductID_ProductID)
                .Index(t => t.ProductID_ProductID);
            
            CreateTable(
                "dbo.CCards",
                c => new
                    {
                        Cardnumber = c.String(nullable: false, maxLength: 128),
                        CCV = c.String(),
                        ExpireYear = c.String(),
                        ExpireMonth = c.String(),
                        Email = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Cardnumber)
                .ForeignKey("dbo.Customers", t => t.Email)
                .Index(t => t.Email);
            
            CreateTable(
                "dbo.CustomerCredentials",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        Password = c.Binary(),
                    })
                .PrimaryKey(t => t.Email);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Address = c.String(),
                        Postal_Zipcode = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Email)
                .ForeignKey("dbo.Postals", t => t.Postal_Zipcode)
                .Index(t => t.Postal_Zipcode);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 128),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Customers", t => t.Email)
                .Index(t => t.Email);
            
            CreateTable(
                "dbo.Orderlines",
                c => new
                    {
                        OrderlineID = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Item_ProductID = c.Int(),
                        Order_OrderID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderlineID)
                .ForeignKey("dbo.Products", t => t.Item_ProductID)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID)
                .Index(t => t.Item_ProductID)
                .Index(t => t.Order_OrderID);
            
            CreateTable(
                "dbo.Postals",
                c => new
                    {
                        Zipcode = c.String(nullable: false, maxLength: 128),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.Zipcode);
            
            CreateTable(
                "dbo.Shoppingcarts",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Email);
            
            CreateTable(
                "dbo.ShoppingcartItems",
                c => new
                    {
                        ShoppingCartItemID = c.Int(nullable: false, identity: true),
                        Item_ProductID = c.Int(),
                        ShoppingCart_Email = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ShoppingCartItemID)
                .ForeignKey("dbo.Products", t => t.Item_ProductID)
                .ForeignKey("dbo.Shoppingcarts", t => t.ShoppingCart_Email)
                .Index(t => t.Item_ProductID)
                .Index(t => t.ShoppingCart_Email);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingcartItems", "ShoppingCart_Email", "dbo.Shoppingcarts");
            DropForeignKey("dbo.ShoppingcartItems", "Item_ProductID", "dbo.Products");
            DropForeignKey("dbo.Customers", "Postal_Zipcode", "dbo.Postals");
            DropForeignKey("dbo.Orders", "Email", "dbo.Customers");
            DropForeignKey("dbo.Orderlines", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orderlines", "Item_ProductID", "dbo.Products");
            DropForeignKey("dbo.CCards", "Email", "dbo.Customers");
            DropForeignKey("dbo.Images", "ProductID_ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.Categories");
            DropIndex("dbo.ShoppingcartItems", new[] { "ShoppingCart_Email" });
            DropIndex("dbo.ShoppingcartItems", new[] { "Item_ProductID" });
            DropIndex("dbo.Orderlines", new[] { "Order_OrderID" });
            DropIndex("dbo.Orderlines", new[] { "Item_ProductID" });
            DropIndex("dbo.Orders", new[] { "Email" });
            DropIndex("dbo.Customers", new[] { "Postal_Zipcode" });
            DropIndex("dbo.CCards", new[] { "Email" });
            DropIndex("dbo.Images", new[] { "ProductID_ProductID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropTable("dbo.ShoppingcartItems");
            DropTable("dbo.Shoppingcarts");
            DropTable("dbo.Postals");
            DropTable("dbo.Orderlines");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
            DropTable("dbo.CustomerCredentials");
            DropTable("dbo.CCards");
            DropTable("dbo.Images");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }
    }
}
