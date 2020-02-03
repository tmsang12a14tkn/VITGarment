namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixdetailrevision : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GoalDetailRevisions", "TotalMinutes", c => c.Int(nullable: false));
            AlterColumn("dbo.GoalDetailRevisions", "ProductId", c => c.String(maxLength: 20));
            CreateIndex("dbo.GoalDetailRevisions", "ProductId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.GoalDetailRevisions", new[] { "ProductId" });
            AlterColumn("dbo.GoalDetailRevisions", "ProductId", c => c.String());
            DropColumn("dbo.GoalDetailRevisions", "TotalMinutes");
        }
    }
}
