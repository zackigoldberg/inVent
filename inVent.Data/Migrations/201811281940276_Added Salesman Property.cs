namespace inVent.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSalesmanProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sale", "Salesman", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sale", "Salesman");
        }
    }
}
