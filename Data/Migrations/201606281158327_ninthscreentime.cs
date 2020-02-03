namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ninthscreentime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamSettings", "NinthScreenTime", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeamSettings", "NinthScreenTime");
        }
    }
}
