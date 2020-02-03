namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedeprecatedgoaldetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GoalDetails", "ReasonId", "dbo.Reasons");
            DropIndex("dbo.GoalDetails", new[] { "ReasonId" });
            DropColumn("dbo.GoalDetails", "Quantity");
            DropColumn("dbo.GoalDetails", "ProduceQuantity");
            DropColumn("dbo.GoalDetails", "ManualProduceQuantity");
            DropColumn("dbo.GoalDetails", "ReasonId");
            DropColumn("dbo.GoalDetails", "Comment");
            DropColumn("dbo.GoalDetails", "ProductId");
            DropColumn("dbo.GoalDetails", "NoError");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GoalDetails", "NoError", c => c.Int(nullable: false));
            AddColumn("dbo.GoalDetails", "ProductId", c => c.String());
            AddColumn("dbo.GoalDetails", "Comment", c => c.String());
            AddColumn("dbo.GoalDetails", "ReasonId", c => c.Int());
            AddColumn("dbo.GoalDetails", "ManualProduceQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.GoalDetails", "ProduceQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.GoalDetails", "Quantity", c => c.Int(nullable: false));
            CreateIndex("dbo.GoalDetails", "ReasonId");
            AddForeignKey("dbo.GoalDetails", "ReasonId", "dbo.Reasons", "Id");
        }
    }
}
