namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bigchangegoal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProduceHistories", "GoalDetailId", "dbo.GoalDetails");
            DropIndex("dbo.ProduceHistories", new[] { "GoalDetailId" });
            CreateTable(
                "dbo.GoalDetailParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GoalDetailId = c.Int(nullable: false),
                        StartCountTime = c.Time(nullable: false, precision: 7),
                        EndCountTime = c.Time(nullable: false, precision: 7),
                        Quantity = c.Int(nullable: false),
                        ProduceQuantity = c.Int(nullable: false),
                        ManualProduceQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GoalDetails", t => t.GoalDetailId, cascadeDelete: true)
                .Index(t => t.GoalDetailId);
            
            CreateTable(
                "dbo.GoalDetailPartTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GoalDetailTemplateId = c.Int(nullable: false),
                        StartCountTime = c.Time(nullable: false, precision: 7),
                        EndCountTime = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GoalDetailTemplates", t => t.GoalDetailTemplateId, cascadeDelete: true)
                .Index(t => t.GoalDetailTemplateId);
            
            AddColumn("dbo.ProduceHistories", "GoalDetailPartId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProduceHistories", "GoalDetailPartId");
            AddForeignKey("dbo.ProduceHistories", "GoalDetailPartId", "dbo.GoalDetailParts", "Id", cascadeDelete: true);
            DropColumn("dbo.ProduceHistories", "GoalDetailId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProduceHistories", "GoalDetailId", c => c.Int(nullable: false));
            DropForeignKey("dbo.GoalDetailPartTemplates", "GoalDetailTemplateId", "dbo.GoalDetailTemplates");
            DropForeignKey("dbo.ProduceHistories", "GoalDetailPartId", "dbo.GoalDetailParts");
            DropForeignKey("dbo.GoalDetailParts", "GoalDetailId", "dbo.GoalDetails");
            DropIndex("dbo.GoalDetailPartTemplates", new[] { "GoalDetailTemplateId" });
            DropIndex("dbo.GoalDetailParts", new[] { "GoalDetailId" });
            DropIndex("dbo.ProduceHistories", new[] { "GoalDetailPartId" });
            DropColumn("dbo.ProduceHistories", "GoalDetailPartId");
            DropTable("dbo.GoalDetailPartTemplates");
            DropTable("dbo.GoalDetailParts");
            CreateIndex("dbo.ProduceHistories", "GoalDetailId");
            AddForeignKey("dbo.ProduceHistories", "GoalDetailId", "dbo.GoalDetails", "Id", cascadeDelete: true);
        }
    }
}
