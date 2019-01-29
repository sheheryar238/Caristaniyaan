namespace Caristaniyaan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "fname", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "lname", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "email", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "phoneno", c => c.String(nullable: false, maxLength: 13));
            AddColumn("dbo.Orders", "address", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "city", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "postcode", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "province", c => c.String(nullable: false));
            AddColumn("dbo.Orders", "countary", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "countary");
            DropColumn("dbo.Orders", "province");
            DropColumn("dbo.Orders", "postcode");
            DropColumn("dbo.Orders", "city");
            DropColumn("dbo.Orders", "address");
            DropColumn("dbo.Orders", "phoneno");
            DropColumn("dbo.Orders", "email");
            DropColumn("dbo.Orders", "lname");
            DropColumn("dbo.Orders", "fname");
        }
    }
}
