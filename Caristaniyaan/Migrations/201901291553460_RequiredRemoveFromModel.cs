namespace Caristaniyaan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredRemoveFromModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "model", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "model", c => c.Int(nullable: false));
        }
    }
}
