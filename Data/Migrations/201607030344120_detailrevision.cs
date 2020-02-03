namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class detailrevision : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GoalDetailRevisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GoalDetailId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ProduceQuantity = c.Int(nullable: false),
                        NoEmployee = c.Int(nullable: false),
                        ReasonId = c.Int(),
                        Comment = c.String(),
                        ProductId = c.String(),
                        UserId = c.String(maxLength: 128),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GoalDetails", t => t.GoalDetailId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUsers", t => t.UserId)
                .Index(t => t.GoalDetailId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GoalDetailRevisions", "UserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.GoalDetailRevisions", "GoalDetailId", "dbo.GoalDetails");
            DropIndex("dbo.GoalDetailRevisions", new[] { "UserId" });
            DropIndex("dbo.GoalDetailRevisions", new[] { "GoalDetailId" });
            DropTable("dbo.GoalDetailRevisions");
        }
    }
}
