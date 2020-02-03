namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class screenssetting : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamSettings", "FirstScreenTime", c => c.Int(nullable: false));
            AddColumn("dbo.TeamSettings", "SecondScreenTime", c => c.Int(nullable: false));
            AddColumn("dbo.TeamSettings", "FourthScreenTime", c => c.Int(nullable: false));
            AddColumn("dbo.TeamSettings", "FifthScreenTime", c => c.Int(nullable: false));
            AddColumn("dbo.TeamSettings", "SixthScreenTime", c => c.Int(nullable: false));
            AddColumn("dbo.TeamSettings", "SeventhScreenTime", c => c.Int(nullable: false));
            AddColumn("dbo.TeamSettings", "EighthScreenTime", c => c.Int(nullable: false));
            AddColumn("dbo.TeamSettings", "ThirdMessage", c => c.String());
            AddColumn("dbo.TeamSettings", "FourthMessage", c => c.String());
            AddColumn("dbo.TeamSettings", "FifthMessage", c => c.String());
            AddColumn("dbo.TeamSettings", "SixthMessage", c => c.String());
            AddColumn("dbo.TeamSettings", "SeventhMessage", c => c.String());
            AddColumn("dbo.TeamSettings", "EighthMessage", c => c.String());
            DropColumn("dbo.TeamSettings", "CustomMessage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeamSettings", "CustomMessage", c => c.String());
            DropColumn("dbo.TeamSettings", "EighthMessage");
            DropColumn("dbo.TeamSettings", "SeventhMessage");
            DropColumn("dbo.TeamSettings", "SixthMessage");
            DropColumn("dbo.TeamSettings", "FifthMessage");
            DropColumn("dbo.TeamSettings", "FourthMessage");
            DropColumn("dbo.TeamSettings", "ThirdMessage");
            DropColumn("dbo.TeamSettings", "EighthScreenTime");
            DropColumn("dbo.TeamSettings", "SeventhScreenTime");
            DropColumn("dbo.TeamSettings", "SixthScreenTime");
            DropColumn("dbo.TeamSettings", "FifthScreenTime");
            DropColumn("dbo.TeamSettings", "FourthScreenTime");
            DropColumn("dbo.TeamSettings", "SecondScreenTime");
            DropColumn("dbo.TeamSettings", "FirstScreenTime");
        }
    }
}
