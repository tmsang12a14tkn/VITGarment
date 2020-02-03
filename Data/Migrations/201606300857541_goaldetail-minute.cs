namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class goaldetailminute : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GoalDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.GoalDetails", new[] { "ProductId" });
            DropPrimaryKey("dbo.Products");
            AddColumn("dbo.GoalDetails", "TotalMinutes", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "ProductId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Reasons", "Order", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Products", "ProductId");
            DropColumn("dbo.GoalDetails", "ProductId");
            DropColumn("dbo.Products", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.GoalDetails", "ProductId", c => c.Int());
            DropPrimaryKey("dbo.Products");
            DropColumn("dbo.Reasons", "Order");
            DropColumn("dbo.Products", "ProductId");
            DropColumn("dbo.GoalDetails", "TotalMinutes");
            AddPrimaryKey("dbo.Products", "Id");
            CreateIndex("dbo.GoalDetails", "ProductId");
            AddForeignKey("dbo.GoalDetails", "ProductId", "dbo.Products", "Id");
        }
    }
}
