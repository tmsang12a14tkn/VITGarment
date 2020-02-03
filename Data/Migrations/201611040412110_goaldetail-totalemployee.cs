namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class goaldetailtotalemployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GoalDetails", "TotalEmployee", c => c.Int(nullable: false));
            AddColumn("dbo.GoalDetails", "SessionOrder", c => c.Int(nullable: false));
            DropColumn("dbo.GoalDetails", "SessionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GoalDetails", "SessionId", c => c.Int(nullable: false));
            DropColumn("dbo.GoalDetails", "SessionOrder");
            DropColumn("dbo.GoalDetails", "TotalEmployee");
        }
    }
}
