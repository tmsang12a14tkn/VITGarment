namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class specdetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QCSpecDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QCSpecificationId = c.Int(nullable: false),
                        Specification = c.String(),
                        Tolerance = c.String(),
                        Parameter1 = c.Single(nullable: false),
                        Parameter2 = c.Single(nullable: false),
                        Parameter3 = c.Single(nullable: false),
                        Parameter4 = c.Single(nullable: false),
                        Parameter5 = c.Single(nullable: false),
                        Parameter6 = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QCSpecifications", t => t.QCSpecificationId, cascadeDelete: true)
                .Index(t => t.QCSpecificationId);
            
            DropColumn("dbo.QCSpecifications", "Specification");
            DropColumn("dbo.QCSpecifications", "Tolerance");
            DropColumn("dbo.QCSpecifications", "Parameter1");
            DropColumn("dbo.QCSpecifications", "Parameter2");
            DropColumn("dbo.QCSpecifications", "Parameter3");
            DropColumn("dbo.QCSpecifications", "Parameter4");
            DropColumn("dbo.QCSpecifications", "Parameter5");
            DropColumn("dbo.QCSpecifications", "Parameter6");
            DropColumn("dbo.QCSpecifications", "Note");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QCSpecifications", "Note", c => c.String());
            AddColumn("dbo.QCSpecifications", "Parameter6", c => c.String());
            AddColumn("dbo.QCSpecifications", "Parameter5", c => c.Single(nullable: false));
            AddColumn("dbo.QCSpecifications", "Parameter4", c => c.Single(nullable: false));
            AddColumn("dbo.QCSpecifications", "Parameter3", c => c.Single(nullable: false));
            AddColumn("dbo.QCSpecifications", "Parameter2", c => c.Single(nullable: false));
            AddColumn("dbo.QCSpecifications", "Parameter1", c => c.Single(nullable: false));
            AddColumn("dbo.QCSpecifications", "Tolerance", c => c.String());
            AddColumn("dbo.QCSpecifications", "Specification", c => c.String());
            DropForeignKey("dbo.QCSpecDetails", "QCSpecificationId", "dbo.QCSpecifications");
            DropIndex("dbo.QCSpecDetails", new[] { "QCSpecificationId" });
            DropTable("dbo.QCSpecDetails");
        }
    }
}
