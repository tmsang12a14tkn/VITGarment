namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qcreportsessionproduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QCErrors", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_QCId" }, "dbo.QCReports");
            DropForeignKey("dbo.QCSpecifications", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_QCId" }, "dbo.QCReports");
            DropIndex("dbo.QCErrors", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_QCId" });
            DropIndex("dbo.QCSpecifications", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_QCId" });
            DropPrimaryKey("dbo.QCReports");
            AddColumn("dbo.QCErrors", "QCReport_SessionOrder", c => c.Int());
            AddColumn("dbo.QCErrors", "QCReport_ProductId", c => c.String(maxLength: 128));
            AddColumn("dbo.QCReports", "SessionOrder", c => c.Int(nullable: false));
            AddColumn("dbo.QCReports", "ProductId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.QCSpecifications", "QCReport_SessionOrder", c => c.Int());
            AddColumn("dbo.QCSpecifications", "QCReport_ProductId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.QCReports", new[] { "TeamId", "Date", "SessionOrder", "ProductId", "QCId" });
            CreateIndex("dbo.QCErrors", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_SessionOrder", "QCReport_ProductId", "QCReport_QCId" });
            CreateIndex("dbo.QCSpecifications", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_SessionOrder", "QCReport_ProductId", "QCReport_QCId" });
            AddForeignKey("dbo.QCErrors", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_SessionOrder", "QCReport_ProductId", "QCReport_QCId" }, "dbo.QCReports", new[] { "TeamId", "Date", "SessionOrder", "ProductId", "QCId" });
            AddForeignKey("dbo.QCSpecifications", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_SessionOrder", "QCReport_ProductId", "QCReport_QCId" }, "dbo.QCReports", new[] { "TeamId", "Date", "SessionOrder", "ProductId", "QCId" });
            DropColumn("dbo.QCReports", "ProductCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QCReports", "ProductCode", c => c.String());
            DropForeignKey("dbo.QCSpecifications", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_SessionOrder", "QCReport_ProductId", "QCReport_QCId" }, "dbo.QCReports");
            DropForeignKey("dbo.QCErrors", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_SessionOrder", "QCReport_ProductId", "QCReport_QCId" }, "dbo.QCReports");
            DropIndex("dbo.QCSpecifications", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_SessionOrder", "QCReport_ProductId", "QCReport_QCId" });
            DropIndex("dbo.QCErrors", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_SessionOrder", "QCReport_ProductId", "QCReport_QCId" });
            DropPrimaryKey("dbo.QCReports");
            DropColumn("dbo.QCSpecifications", "QCReport_ProductId");
            DropColumn("dbo.QCSpecifications", "QCReport_SessionOrder");
            DropColumn("dbo.QCReports", "ProductId");
            DropColumn("dbo.QCReports", "SessionOrder");
            DropColumn("dbo.QCErrors", "QCReport_ProductId");
            DropColumn("dbo.QCErrors", "QCReport_SessionOrder");
            AddPrimaryKey("dbo.QCReports", new[] { "TeamId", "Date", "QCId" });
            CreateIndex("dbo.QCSpecifications", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_QCId" });
            CreateIndex("dbo.QCErrors", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_QCId" });
            AddForeignKey("dbo.QCSpecifications", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_QCId" }, "dbo.QCReports", new[] { "TeamId", "Date", "QCId" });
            AddForeignKey("dbo.QCErrors", new[] { "QCReport_TeamId", "QCReport_Date", "QCReport_QCId" }, "dbo.QCReports", new[] { "TeamId", "Date", "QCId" });
        }
    }
}
