namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manualproducequantity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GoalDetails", "ManualProduceQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GoalDetails", "ManualProduceQuantity");
        }
    }
}
