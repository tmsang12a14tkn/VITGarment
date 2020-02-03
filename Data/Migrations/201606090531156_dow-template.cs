namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dowtemplate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GoalDetailTemplates", "DayOfWeek", c => c.Int(nullable: false, defaultValue: (int)DayOfWeek.Monday));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GoalDetailTemplates", "DayOfWeek");
        }
    }
}
