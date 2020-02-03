namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class noerror : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GoalDetails", "NoError", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GoalDetails", "NoError");
        }
    }
}
