namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teamsettings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamSettings",
                c => new
                    {
                        TeamId = c.Int(nullable: false, identity: true),
                        DisplayMode = c.Int(nullable: false),
                        PrimaryScreenId = c.Int(nullable: false),
                        SecondaryScreenId = c.Int(nullable: false),
                        SecondaryScreenEndTime = c.DateTime(nullable: false),
                        CustomMessage = c.String(),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.TeamId)
                .ForeignKey("dbo.Teams", t => t.Team_Id)
                .Index(t => t.Team_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamSettings", "Team_Id", "dbo.Teams");
            DropIndex("dbo.TeamSettings", new[] { "Team_Id" });
            DropTable("dbo.TeamSettings");
        }
    }
}
