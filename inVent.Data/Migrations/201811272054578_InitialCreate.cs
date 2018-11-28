namespace inVent.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Facility",
                c => new
                    {
                        FacilityId = c.Int(nullable: false, identity: true),
                        OwnerId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Type = c.Int(nullable: false),
                        Opened = c.DateTimeOffset(nullable: false, precision: 7),
                        Closed = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.FacilityId);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        ItemNumber = c.Int(nullable: false),
                        Name = c.String(),
                        Stock = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Facility_FacilityId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Facility", t => t.Facility_FacilityId)
                .Index(t => t.Facility_FacilityId);
            
            CreateTable(
                "dbo.Sale",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantitySold = c.Int(nullable: false),
                        Facility_FacilityId = c.Int(),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("dbo.Item", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Facility", t => t.Facility_FacilityId)
                .Index(t => t.UserId)
                .Index(t => t.Facility_FacilityId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RoleId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Sale", "Facility_FacilityId", "dbo.Facility");
            DropForeignKey("dbo.Sale", "UserId", "dbo.Item");
            DropForeignKey("dbo.Item", "Facility_FacilityId", "dbo.Facility");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Sale", new[] { "Facility_FacilityId" });
            DropIndex("dbo.Sale", new[] { "UserId" });
            DropIndex("dbo.Item", new[] { "Facility_FacilityId" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Sale");
            DropTable("dbo.Item");
            DropTable("dbo.Facility");
        }
    }
}
