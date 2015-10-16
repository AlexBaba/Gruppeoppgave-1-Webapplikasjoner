namespace Nettbutikk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductImageUrl : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "MainImage_Path", "dbo.Images");
            DropIndex("dbo.Products", new[] { "MainImage_Path" });
            AddColumn("dbo.Products", "ImageUrl", c => c.String());
            DropColumn("dbo.Products", "MainImage_Path");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "MainImage_Path", c => c.String(maxLength: 128));
            DropColumn("dbo.Products", "ImageUrl");
            CreateIndex("dbo.Products", "MainImage_Path");
            AddForeignKey("dbo.Products", "MainImage_Path", "dbo.Images", "Path");
        }
    }
}
