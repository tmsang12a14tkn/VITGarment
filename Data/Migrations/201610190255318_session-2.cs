namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class session2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FactorySessions", "Start", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.TeamSessions", "Start", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.FactorySessions", "From");
            DropColumn("dbo.TeamSessions", "From");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeamSessions", "From", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.FactorySessions", "From", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.TeamSessions", "Start");
            DropColumn("dbo.FactorySessions", "Start");
        }
    }
}
