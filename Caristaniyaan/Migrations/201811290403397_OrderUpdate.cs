namespace Caristaniyaan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "province", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "countary", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "countary", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "province", c => c.String(nullable: false));
        }
    }
}
