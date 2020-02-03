namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productdetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GoalDetailId = c.Int(nullable: false),
                        ProductId = c.String(),
                        TotalMinutes = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ProduceQuantity = c.Int(nullable: false),
                        ReasonId = c.Int(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GoalDetails", t => t.GoalDetailId, cascadeDelete: true)
                .ForeignKey("dbo.Reasons", t => t.ReasonId)
                .Index(t => t.GoalDetailId)
                .Index(t => t.ReasonId);
            
            AddColumn("dbo.Goals", "NoEmployee", c => c.Int(nullable: false));
            RenameColumn("dbo.Teams", "NumOfWorker", "NoEmployee");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Teams", "NumOfWorker", c => c.Int(nullable: false));
            DropForeignKey("dbo.ProductDetails", "ReasonId", "dbo.Reasons");
            DropForeignKey("dbo.ProductDetails", "GoalDetailId", "dbo.GoalDetails");
            DropIndex("dbo.ProductDetails", new[] { "ReasonId" });
            DropIndex("dbo.ProductDetails", new[] { "GoalDetailId" });
            //DropColumn("dbo.Teams", "NoEmployee");
            DropColumn("dbo.Goals", "NoEmployee");
            DropTable("dbo.ProductDetails");
            RenameColumn("dbo.Teams", "NoEmployee", "NumOfWorker");
        }
    }
}
