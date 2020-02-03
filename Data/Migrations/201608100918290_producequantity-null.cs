namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class producequantitynull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductDetails", "ProduceQuantity", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductDetails", "ProduceQuantity", c => c.Int(nullable: false));
        }
    }
}
