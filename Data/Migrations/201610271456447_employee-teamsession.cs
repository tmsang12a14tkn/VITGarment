namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employeeteamsession : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamSessions", "NoEmployee", c => c.Int(nullable: false));
            AddColumn("dbo.TeamSessions", "TotalEmployee", c => c.Int(nullable: false));
            AddColumn("dbo.TeamSessions", "AbsentWithoutReason", c => c.Int(nullable: false));
            AddColumn("dbo.TeamSessions", "AbsentComment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeamSessions", "AbsentComment");
            DropColumn("dbo.TeamSessions", "AbsentWithoutReason");
            DropColumn("dbo.TeamSessions", "TotalEmployee");
            DropColumn("dbo.TeamSessions", "NoEmployee");
        }
    }
}
