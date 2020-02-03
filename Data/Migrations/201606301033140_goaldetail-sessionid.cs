namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class goaldetailsessionid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GoalDetails", "SessionId", c => c.Int(nullable: false));
            AddColumn("dbo.GoalDetailTemplates", "SessionId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GoalDetailTemplates", "SessionId");
            DropColumn("dbo.GoalDetails", "SessionId");
        }
    }
}
