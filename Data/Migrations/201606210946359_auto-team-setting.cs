namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class autoteamsetting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamSettings", "AutoScreenStartTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeamSettings", "AutoScreenStartTime");
        }
    }
}
