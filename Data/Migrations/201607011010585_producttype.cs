namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class producttype : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Goals", "CreateTime", c => c.DateTime());
            AddColumn("dbo.Goals", "LastestUpdateTime", c => c.DateTime());
            AddColumn("dbo.Goals", "CreateUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.Products", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Duration", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "ProductTypeId", c => c.Int());
            CreateIndex("dbo.Goals", "CreateUserId");
            CreateIndex("dbo.Products", "ProductTypeId");
            AddForeignKey("dbo.Goals", "CreateUserId", "dbo.ApplicationUsers", "Id");
            AddForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.Goals", "CreateUserId", "dbo.ApplicationUsers");
            DropIndex("dbo.Products", new[] { "ProductTypeId" });
            DropIndex("dbo.Goals", new[] { "CreateUserId" });
            DropColumn("dbo.Products", "ProductTypeId");
            DropColumn("dbo.Products", "Duration");
            DropColumn("dbo.Products", "Quantity");
            DropColumn("dbo.Products", "Gender");
            DropColumn("dbo.Goals", "CreateUserId");
            DropColumn("dbo.Goals", "LastestUpdateTime");
            DropColumn("dbo.Goals", "CreateTime");
            DropTable("dbo.ProductTypes");
        }
    }
}
