namespace inVent.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Donu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "Description", c => c.String());
            AddColumn("dbo.Sale", "SaleTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Item", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Sale", "Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sale", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Item", "Name", c => c.String());
            DropColumn("dbo.Sale", "SaleTotal");
            DropColumn("dbo.Item", "Description");
        }
    }
}
