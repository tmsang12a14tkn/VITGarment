namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixreasonid : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.GoalDetailRevisions", "ReasonId");
            AddForeignKey("dbo.GoalDetailRevisions", "ReasonId", "dbo.Reasons", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GoalDetailRevisions", "ReasonId", "dbo.Reasons");
            DropIndex("dbo.GoalDetailRevisions", new[] { "ReasonId" });
        }
    }
}
