namespace Nettbutikk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageURL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "ImageURL", c => c.String());
            DropColumn("dbo.Images", "Imagebytes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Imagebytes", c => c.Binary());
            DropColumn("dbo.Images", "ImageURL");
        }
    }
}
