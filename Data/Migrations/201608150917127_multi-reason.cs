namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class multireason : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductDetails", "ReasonId", "dbo.Reasons");
            DropIndex("dbo.ProductDetails", new[] { "ReasonId" });
            CreateTable(
                "dbo.ReasonProductDetails",
                c => new
                    {
                        Reason_Id = c.Int(nullable: false),
                        ProductDetail_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Reason_Id, t.ProductDetail_Id })
                .ForeignKey("dbo.Reasons", t => t.Reason_Id, cascadeDelete: true)
                .ForeignKey("dbo.ProductDetails", t => t.ProductDetail_Id, cascadeDelete: true)
                .Index(t => t.Reason_Id)
                .Index(t => t.ProductDetail_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReasonProductDetails", "ProductDetail_Id", "dbo.ProductDetails");
            DropForeignKey("dbo.ReasonProductDetails", "Reason_Id", "dbo.Reasons");
            DropIndex("dbo.ReasonProductDetails", new[] { "ProductDetail_Id" });
            DropIndex("dbo.ReasonProductDetails", new[] { "Reason_Id" });
            DropTable("dbo.ReasonProductDetails");
            CreateIndex("dbo.ProductDetails", "ReasonId");
            AddForeignKey("dbo.ProductDetails", "ReasonId", "dbo.Reasons", "Id");
        }
    }
}
