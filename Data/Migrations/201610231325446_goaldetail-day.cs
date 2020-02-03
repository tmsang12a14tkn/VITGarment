namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class goaldetailday : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GoalDetails", "StartCountDay", c => c.Int(nullable: false));
            AddColumn("dbo.GoalDetails", "EndCountDay", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GoalDetails", "EndCountDay");
            DropColumn("dbo.GoalDetails", "StartCountDay");
        }
    }
}
