namespace Nettbutikk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Another : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderLines", "Amount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderLines", "Amount");
        }
    }
}
