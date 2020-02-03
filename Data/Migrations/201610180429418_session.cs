namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class session : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FactorySessions",
                c => new
                    {
                        FactoryId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Order = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        From = c.Time(nullable: false, precision: 7),
                        End = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => new { t.FactoryId, t.Date, t.Order })
                .ForeignKey("dbo.Factories", t => t.FactoryId, cascadeDelete: true)
                .Index(t => t.FactoryId);
            
            CreateTable(
                "dbo.TeamSessions",
                c => new
                    {
                        TeamId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Order = c.Int(nullable: false),
                        From = c.Time(nullable: false, precision: 7),
                        End = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => new { t.TeamId, t.Date, t.Order })
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamSessions", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.FactorySessions", "FactoryId", "dbo.Factories");
            DropIndex("dbo.TeamSessions", new[] { "TeamId" });
            DropIndex("dbo.FactorySessions", new[] { "FactoryId" });
            DropTable("dbo.TeamSessions");
            DropTable("dbo.FactorySessions");
        }
    }
}
