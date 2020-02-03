namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class producequantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GoalDetails", "ProduceQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GoalDetails", "ProduceQuantity");
        }
    }
}
