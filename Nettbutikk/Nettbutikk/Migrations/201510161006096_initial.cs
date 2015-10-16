namespace Nettbutikk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
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
                        PaymentCard_Number = c.String(maxLength: 128),
                        PrimaryBillingAddress_Id = c.Guid(),
                        PrimaryShippingAddress_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CreditCards", t => t.PaymentCard_Number)
                .ForeignKey("dbo.Addresses", t => t.PrimaryBillingAddress_Id)
                .ForeignKey("dbo.Addresses", t => t.PrimaryShippingAddress_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.PaymentCard_Number)
                .Index(t => t.PrimaryBillingAddress_Id)
                .Index(t => t.PrimaryShippingAddress_Id);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        StreetName = c.String(nullable: false),
                        HouseNumber = c.String(nullable: false),
                        State = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        ZipCode_Code = c.String(nullable: false, maxLength: 128),
                        Addressee_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ZipCodes", t => t.ZipCode_Code, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.Addressee_Id, cascadeDelete: false)
                .Index(t => t.ZipCode_Code)
                .Index(t => t.Addressee_Id);
            
            CreateTable(
                "dbo.ZipCodes",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 128),
                        City = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
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
                        ShippingAddress_Id = c.Guid(nullable: false),
                        Customer_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.BillingAddress_Id, cascadeDelete: false)
                .ForeignKey("dbo.Addresses", t => t.ShippingAddress_Id, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.Customer_Id, cascadeDelete: false)
                .Index(t => t.BillingAddress_Id)
                .Index(t => t.ShippingAddress_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Product_Id = c.Int(nullable: false),
                        Order_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: false)
                .ForeignKey("dbo.Orders", t => t.Order_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Single(nullable: false),
                        Description = c.String(nullable: false),
                        ImageUrl = c.String(),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        ParentCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.ParentCategory_Id)
                .Index(t => t.ParentCategory_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Path = c.String(nullable: false, maxLength: 128),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Path)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        Number = c.String(nullable: false, maxLength: 128),
                        NameOnCard = c.String(nullable: false),
                        CCV = c.String(nullable: false, maxLength: 3),
                        ExpireYear = c.String(nullable: false, maxLength: 2),
                        ExpireMonth = c.String(nullable: false, maxLength: 2),
                    })
                .PrimaryKey(t => t.Number);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "PrimaryShippingAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.AspNetUsers", "PrimaryBillingAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.AspNetUsers", "PaymentCard_Number", "dbo.CreditCards");
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "ShippingAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.OrderLines", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.OrderLines", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Images", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentCategory_Id", "dbo.Categories");
            DropForeignKey("dbo.Orders", "BillingAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Addresses", "Addressee_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Addresses", "ZipCode_Code", "dbo.ZipCodes");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.Images", new[] { "Product_Id" });
            DropIndex("dbo.Categories", new[] { "ParentCategory_Id" });
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropIndex("dbo.OrderLines", new[] { "Order_Id" });
            DropIndex("dbo.OrderLines", new[] { "Product_Id" });
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            DropIndex("dbo.Orders", new[] { "ShippingAddress_Id" });
            DropIndex("dbo.Orders", new[] { "BillingAddress_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Addresses", new[] { "Addressee_Id" });
            DropIndex("dbo.Addresses", new[] { "ZipCode_Code" });
            DropIndex("dbo.AspNetUsers", new[] { "PrimaryShippingAddress_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "PrimaryBillingAddress_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "PaymentCard_Number" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.CreditCards");
            DropTable("dbo.Images");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.OrderLines");
            DropTable("dbo.Orders");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.ZipCodes");
            DropTable("dbo.Addresses");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
