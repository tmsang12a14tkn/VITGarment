namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixgoaldetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GoalDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.GoalDetails", "ReasonId", "dbo.Reasons");
            DropIndex("dbo.GoalDetails", new[] { "ReasonId" });
            DropIndex("dbo.GoalDetails", new[] { "ProductId" });
            AlterColumn("dbo.GoalDetails", "ReasonId", c => c.Int());
            AlterColumn("dbo.GoalDetails", "ProductId", c => c.Int());
            CreateIndex("dbo.GoalDetails", "ReasonId");
            CreateIndex("dbo.GoalDetails", "ProductId");
            AddForeignKey("dbo.GoalDetails", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.GoalDetails", "ReasonId", "dbo.Reasons", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GoalDetails", "ReasonId", "dbo.Reasons");
            DropForeignKey("dbo.GoalDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.GoalDetails", new[] { "ProductId" });
            DropIndex("dbo.GoalDetails", new[] { "ReasonId" });
            AlterColumn("dbo.GoalDetails", "ProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.GoalDetails", "ReasonId", c => c.Int(nullable: false));
            CreateIndex("dbo.GoalDetails", "ProductId");
            CreateIndex("dbo.GoalDetails", "ReasonId");
            AddForeignKey("dbo.GoalDetails", "ReasonId", "dbo.Reasons", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GoalDetails", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
