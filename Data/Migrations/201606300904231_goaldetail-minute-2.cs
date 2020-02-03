namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class goaldetailminute2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GoalDetails", "ProductId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GoalDetails", "ProductId");
        }
    }
}
