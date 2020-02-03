namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hide : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GoalDetails", "Hide", c => c.Boolean(nullable: false));
            DropColumn("dbo.GoalDetails", "Visible");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GoalDetails", "Visible", c => c.Boolean(nullable: false));
            DropColumn("dbo.GoalDetails", "Hide");
        }
    }
}
