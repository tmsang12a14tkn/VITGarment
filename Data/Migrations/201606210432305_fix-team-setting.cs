namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixteamsetting : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamSettings", "Team_Id", "dbo.Teams");
            DropIndex("dbo.TeamSettings", new[] { "Team_Id" });
            DropColumn("dbo.TeamSettings", "Team_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeamSettings", "Team_Id", c => c.Int());
            CreateIndex("dbo.TeamSettings", "Team_Id");
            AddForeignKey("dbo.TeamSettings", "Team_Id", "dbo.Teams", "Id");
        }
    }
}
