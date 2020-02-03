namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class factorysessionday : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FactorySessions", "StartDay", c => c.Int(nullable: false));
            AddColumn("dbo.FactorySessions", "EndDay", c => c.Int(nullable: false));
            AddColumn("dbo.TeamSessions", "StartDay", c => c.Int(nullable: false));
            AddColumn("dbo.TeamSessions", "EndDay", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeamSessions", "EndDay");
            DropColumn("dbo.TeamSessions", "StartDay");
            DropColumn("dbo.FactorySessions", "EndDay");
            DropColumn("dbo.FactorySessions", "StartDay");
        }
    }
}
