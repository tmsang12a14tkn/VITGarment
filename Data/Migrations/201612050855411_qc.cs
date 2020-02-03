namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QCTeams",
                c => new
                    {
                        TeamId = c.Int(nullable: false),
                        QCId = c.Int(nullable: false),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.TeamId, t.QCId })
                .ForeignKey("dbo.QCs", t => t.QCId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.QCId);
            
            CreateTable(
                "dbo.QCs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Code = c.String(),
                        Supplier = c.String(),
                        Visible = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QCErrors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ErrorPosition = c.String(),
                        FailCheckCount = c.Int(nullable: false),
                        FailProductCount = c.Int(nullable: false),
                        FixedProductCount = c.Int(nullable: false),
                        Reason = c.String(),
                        FixMethod = c.String(),
                        QCReport_TeamId = c.Int(),
                        QCReport_Date = c.DateTime(),
                        QCReport_QCId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QCReports", t => new { t.QCReport_TeamId, t.QCReport_Date, t.QCReport_QCId })
                .Index(t => new { t.QCReport_TeamId, t.QCReport_Date, t.QCReport_QCId });
            
            CreateTable(
                "dbo.QCReports",
                c => new
                    {
                        TeamId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        QCId = c.Int(nullable: false),
                        ManagementCode = c.String(),
                        ProductCode = c.String(),
                        Color = c.String(),
                        ProductCount = c.Int(nullable: false),
                        FailProductCount = c.Int(nullable: false),
                        GoodProductCount = c.Int(nullable: false),
                        FailCheckCount = c.Int(nullable: false),
                        AccordingDocument = c.Boolean(nullable: false),
                        AccordingColorTable = c.Boolean(nullable: false),
                        AccordingComment = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.TeamId, t.Date, t.QCId })
                .ForeignKey("dbo.QCs", t => t.QCId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.QCId);
            
            CreateTable(
                "dbo.QCSpecifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Size = c.String(),
                        Specification = c.String(),
                        Tolerance = c.String(),
                        Parameter1 = c.Single(nullable: false),
                        Parameter2 = c.Single(nullable: false),
                        Parameter3 = c.Single(nullable: false),
                        Parameter4 = c.Single(nullable: false),
                        Parameter5 = c.Single(nullable: false),
                        Parameter6 = c.String(),
                        Note = c.String(),
                        QCReport_TeamId = c.Int(),
                        QCReport_Date = c.DateTime(),
                        QCReport_QCId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QCReports", t => new { t.QCReport_TeamId, t.QCReport_Date, t.QCReport_QCId })
                .Index(t => new { t.QCReport_TeamId, t.QCReport_Date, t.QCReport_QCId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QCReports", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.QCSpecifications", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_QCId" }, "dbo.QCReports");
            DropForeignKey("dbo.QCReports", "QCId", "dbo.QCs");
            DropForeignKey("dbo.QCErrors", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_QCId" }, "dbo.QCReports");
            DropForeignKey("dbo.QCTeams", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.QCTeams", "QCId", "dbo.QCs");
            DropIndex("dbo.QCSpecifications", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_QCId" });
            DropIndex("dbo.QCReports", new[] { "QCId" });
            DropIndex("dbo.QCReports", new[] { "TeamId" });
            DropIndex("dbo.QCErrors", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_QCId" });
            DropIndex("dbo.QCTeams", new[] { "QCId" });
            DropIndex("dbo.QCTeams", new[] { "TeamId" });
            DropTable("dbo.QCSpecifications");
            DropTable("dbo.QCReports");
            DropTable("dbo.QCErrors");
            DropTable("dbo.QCs");
            DropTable("dbo.QCTeams");
        }
    }
}
