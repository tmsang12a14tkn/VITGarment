namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixteamsetting2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TeamSettings");
            AlterColumn("dbo.TeamSettings", "TeamId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.TeamSettings", "TeamId");
            CreateIndex("dbo.TeamSettings", "TeamId");
            AddForeignKey("dbo.TeamSettings", "TeamId", "dbo.Teams", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamSettings", "TeamId", "dbo.Teams");
            DropIndex("dbo.TeamSettings", new[] { "TeamId" });
            DropPrimaryKey("dbo.TeamSettings");
            AlterColumn("dbo.TeamSettings", "TeamId", c => c.Int(nullable: false, identity: false));
            AddPrimaryKey("dbo.TeamSettings", "TeamId");
        }
    }
}
