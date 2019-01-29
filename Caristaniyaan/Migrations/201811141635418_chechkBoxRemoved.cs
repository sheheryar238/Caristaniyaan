namespace Caristaniyaan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chechkBoxRemoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Appointments", "terms");
            DropColumn("dbo.Demands", "terms");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Demands", "terms", c => c.Boolean(nullable: false));
            AddColumn("dbo.Appointments", "terms", c => c.Boolean(nullable: false));
        }
    }
}
