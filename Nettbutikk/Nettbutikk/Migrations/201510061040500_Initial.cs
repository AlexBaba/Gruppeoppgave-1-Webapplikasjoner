namespace Nettbutikk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Street = c.String(nullable: false),
                        HouseNumber = c.String(nullable: false),
                        ZipCode = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        Addressee_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Addressee_Id, cascadeDelete: false)
                .Index(t => t.User_Id)
                .Index(t => t.Addressee_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        PrimaryBillingAddress_Id = c.Guid(),
                        PrimaryShippingAddress_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.PrimaryBillingAddress_Id)
                .ForeignKey("dbo.Addresses", t => t.PrimaryShippingAddress_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.PrimaryBillingAddress_Id)
                .Index(t => t.PrimaryShippingAddress_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        PlacementDateTime = c.DateTime(nullable: false),
                        BillingAddress_Id = c.Guid(nullable: false),
                        Customer_Id = c.String(nullable: false, maxLength: 128),
                        ShippingAddress_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.BillingAddress_Id, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.Customer_Id, cascadeDelete: false)
                .ForeignKey("dbo.Addresses", t => t.ShippingAddress_Id, cascadeDelete: false)
                .Index(t => t.BillingAddress_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.ShippingAddress_Id);
            
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Order_Id = c.Guid(nullable: false),
                        Product_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: false)
                .Index(t => t.Order_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        Price = c.Single(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Name)
                .Index(t => t.Name);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Description = c.String(nullable: false),
                        ParentCategory_Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.Categories", t => t.ParentCategory_Name)
                .Index(t => t.ParentCategory_Name);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Addresses", "Addressee_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "PrimaryShippingAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.AspNetUsers", "PrimaryBillingAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.Orders", "ShippingAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.OrderLines", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "Name", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentCategory_Name", "dbo.Categories");
            DropForeignKey("dbo.OrderLines", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "BillingAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Addresses", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Categories", new[] { "ParentCategory_Name" });
            DropIndex("dbo.Products", new[] { "Name" });
            DropIndex("dbo.OrderLines", new[] { "Product_Id" });
            DropIndex("dbo.OrderLines", new[] { "Order_Id" });
            DropIndex("dbo.Orders", new[] { "ShippingAddress_Id" });
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            DropIndex("dbo.Orders", new[] { "BillingAddress_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "PrimaryShippingAddress_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "PrimaryBillingAddress_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Addresses", new[] { "Addressee_Id" });
            DropIndex("dbo.Addresses", new[] { "User_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.OrderLines");
            DropTable("dbo.Orders");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Addresses");
        }
    }
}
