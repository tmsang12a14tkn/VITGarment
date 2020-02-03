namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class settingcolor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamSettings", "ThirdColor", c => c.String());
            AddColumn("dbo.TeamSettings", "FourthColor", c => c.String());
            AddColumn("dbo.TeamSettings", "FifthColor", c => c.String());
            AddColumn("dbo.TeamSettings", "SixthColor", c => c.String());
            AddColumn("dbo.TeamSettings", "SeventhColor", c => c.String());
            AddColumn("dbo.TeamSettings", "EighthColor", c => c.String());
            AddColumn("dbo.TeamSettings", "DefaultUpMessage", c => c.String());
            AddColumn("dbo.TeamSettings", "DefaultDownMessage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeamSettings", "DefaultDownMessage");
            DropColumn("dbo.TeamSettings", "DefaultUpMessage");
            DropColumn("dbo.TeamSettings", "EighthColor");
            DropColumn("dbo.TeamSettings", "SeventhColor");
            DropColumn("dbo.TeamSettings", "SixthColor");
            DropColumn("dbo.TeamSettings", "FifthColor");
            DropColumn("dbo.TeamSettings", "FourthColor");
            DropColumn("dbo.TeamSettings", "ThirdColor");
        }
    }
}
