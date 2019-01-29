namespace Caristaniyaan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbUpdate2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "ServiceId", "dbo.Services");
            DropIndex("dbo.Appointments", new[] { "ServiceId" });
            AddColumn("dbo.Appointments", "modalYear", c => c.String(nullable: false, maxLength: 4));
            AddColumn("dbo.Appointments", "carInfo", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Appointments", "name", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.Appointments", "phonenumber", c => c.String(nullable: false, maxLength: 13));
            AddColumn("dbo.Appointments", "email", c => c.String(nullable: false));
            AddColumn("dbo.Appointments", "message", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.Appointments", "terms", c => c.Boolean(nullable: false));
            AddColumn("dbo.Demands", "modalYear", c => c.String(nullable: false, maxLength: 4));
            AddColumn("dbo.Demands", "carInfo", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Demands", "phonenumber", c => c.String(nullable: false, maxLength: 13));
            AddColumn("dbo.Demands", "itemName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Demands", "itemDetail", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.Demands", "itemImage", c => c.String(nullable: false));
            AddColumn("dbo.Demands", "terms", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Demands", "name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Demands", "email", c => c.String(nullable: false));
            DropColumn("dbo.Appointments", "ServiceId");
            DropColumn("dbo.Appointments", "details");
            DropColumn("dbo.Demands", "phoneno");
            DropColumn("dbo.Demands", "details");
            DropTable("dbo.Services");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        details = c.String(nullable: false),
                        status = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Demands", "details", c => c.String(nullable: false));
            AddColumn("dbo.Demands", "phoneno", c => c.String(nullable: false));
            AddColumn("dbo.Appointments", "details", c => c.String(nullable: false));
            AddColumn("dbo.Appointments", "ServiceId", c => c.Int(nullable: false));
            AlterColumn("dbo.Demands", "email", c => c.String());
            AlterColumn("dbo.Demands", "name", c => c.String(nullable: false));
            DropColumn("dbo.Demands", "terms");
            DropColumn("dbo.Demands", "itemImage");
            DropColumn("dbo.Demands", "itemDetail");
            DropColumn("dbo.Demands", "itemName");
            DropColumn("dbo.Demands", "phonenumber");
            DropColumn("dbo.Demands", "carInfo");
            DropColumn("dbo.Demands", "modalYear");
            DropColumn("dbo.Appointments", "terms");
            DropColumn("dbo.Appointments", "message");
            DropColumn("dbo.Appointments", "email");
            DropColumn("dbo.Appointments", "phonenumber");
            DropColumn("dbo.Appointments", "name");
            DropColumn("dbo.Appointments", "carInfo");
            DropColumn("dbo.Appointments", "modalYear");
            CreateIndex("dbo.Appointments", "ServiceId");
            AddForeignKey("dbo.Appointments", "ServiceId", "dbo.Services", "Id", cascadeDelete: true);
        }
    }
}
