namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstinit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.ProduceHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GoalDetailId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.GoalDetails", t => t.GoalDetailId, cascadeDelete: true)
                .Index(t => t.GoalDetailId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.GoalDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GoalId = c.Int(nullable: false),
                        StartCountTime = c.DateTime(nullable: false),
                        EndCountTime = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goals", t => t.GoalId, cascadeDelete: true)
                .Index(t => t.GoalId);
            
            CreateTable(
                "dbo.Goals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GoalDate = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        StartHour = c.DateTime(nullable: false),
                        EndHour = c.DateTime(nullable: false),
                        TotalHour = c.Int(nullable: false),
                        QuantityPerHour = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: false)
                .Index(t => t.TeamId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.LatestSummaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SummaryDate = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        QuantityPerHour = c.Int(nullable: false),
                        SummaryHour = c.Int(nullable: false),
                        GoalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goals", t => t.GoalId, cascadeDelete: true)
                .Index(t => t.GoalId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Lot = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EmployeeId = c.Int(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUsers", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.Employees", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.ProduceHistories", "GoalDetailId", "dbo.GoalDetails");
            DropForeignKey("dbo.GoalDetails", "GoalId", "dbo.Goals");
            DropForeignKey("dbo.Goals", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Goals", "ProductId", "dbo.Products");
            DropForeignKey("dbo.LatestSummaries", "GoalId", "dbo.Goals");
            DropForeignKey("dbo.ProduceHistories", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUsers", new[] { "EmployeeId" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.LatestSummaries", new[] { "GoalId" });
            DropIndex("dbo.Goals", new[] { "ProductId" });
            DropIndex("dbo.Goals", new[] { "TeamId" });
            DropIndex("dbo.GoalDetails", new[] { "GoalId" });
            DropIndex("dbo.ProduceHistories", new[] { "EmployeeId" });
            DropIndex("dbo.ProduceHistories", new[] { "GoalDetailId" });
            DropIndex("dbo.Employees", new[] { "TeamId" });
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.Teams");
            DropTable("dbo.Products");
            DropTable("dbo.LatestSummaries");
            DropTable("dbo.Goals");
            DropTable("dbo.GoalDetails");
            DropTable("dbo.ProduceHistories");
            DropTable("dbo.Employees");
        }
    }
}
