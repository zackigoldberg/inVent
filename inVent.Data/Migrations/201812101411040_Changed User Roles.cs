namespace inVent.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedUserRoles : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.IdentityUserRole");
            AlterColumn("dbo.IdentityUserRole", "RoleId", c => c.String());
            AlterColumn("dbo.IdentityUserRole", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.IdentityUserRole", "UserId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.IdentityUserRole");
            AlterColumn("dbo.IdentityUserRole", "UserId", c => c.String());
            AlterColumn("dbo.IdentityUserRole", "RoleId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.IdentityUserRole", "RoleId");
        }
    }
}
