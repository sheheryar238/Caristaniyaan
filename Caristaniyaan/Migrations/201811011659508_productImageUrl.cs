namespace Caristaniyaan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productImageUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "image_url", c => c.String(nullable: false));
            DropColumn("dbo.Products", "images");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "images", c => c.String(nullable: false));
            DropColumn("dbo.Products", "image_url");
        }
    }
}
