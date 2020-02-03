namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class visible : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GoalDetails", "Visible", c => c.Boolean(nullable: false));
            AddColumn("dbo.GoalDetailRevisions", "NoError", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GoalDetailRevisions", "NoError");
            DropColumn("dbo.GoalDetails", "Visible");
        }
    }
}
